using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Derin.Business.BusinessLogic.Locator;
using Derin.Business.ViewModel.Login;
using Derin.Common;
using Derin.Data.Model;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Derin.Web.Areas.Auth.Controllers
{
    [Area("Auth")]
    public class LoginController : BaseController
    {

        private AuthBLLocator _authLocator;
        private AdministrationBLLocator _adminlocator;
        public LoginController(AuthBLLocator authLocator, AdministrationBLLocator adminLocator)
        {
            _authLocator = authLocator;
            _adminlocator = adminLocator;
        }

        public IActionResult Unauthorized()
        {

            var authInfo = HttpContext.User.Identity;

            if (authInfo.IsAuthenticated == true)
                return RedirectToAction("Start", "Dashboard", new { area = "Main" });

            return View();
        }

        public async Task<IActionResult> SignOut()
        {
            HttpContext.Session.Clear();
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Unauthorized");
        }


        [HttpPost]
        public async Task<IActionResult> Unauthorized(LoginVM loginVM)
        {

            /*
             ErrorCode
             0: Başarılı
            -1: Kullanıcı Bilgisi Alınamadı!
            -2: Kullanıcı Pasif!
            -3: Hatalı Şifre!
            -4: Bloklanmış Kullanıcı!
             */
            try
            {
                if (ModelState.IsValid)
                {
                    SystemUser systemUser = null;
                    string password = "";

                    systemUser = _adminlocator.SystemUserBL.CRUD.Get(x => x.Username == loginVM.UserName).FirstOrDefault();
                    password = loginVM.Password;

                    if (systemUser != null)
                    {


                        if (HashingParameter.ValidateSHA1HashData(password, systemUser.Password))
                        {
                            if (systemUser.Status == 1) // 1 ise pasif!
                            {
                                ConnectionLog(-2, loginVM.UserName, systemUser.IdUser);
                                ModelState.AddModelError("Error", "Kullanıcı Pasif Edilmiştir!");
                                return View();
                            }
                            else if (systemUser.Status == 2) // 2 ise bloklanmış!
                            {
                                ConnectionLog(-4, loginVM.UserName, systemUser.IdUser);
                                ModelState.AddModelError("Error", "Kullanıcı Bloklanmıştır!");
                                return View();
                            }
                            var roleDetails = _adminlocator.RoleBL.GetUserRoleDetails(systemUser.IdUser);
                            if (roleDetails.Count < 1)
                            {
                                ModelState.AddModelError("Error", "Sistemde Kullanıcı Rol Tanımı Bulunamadı!");
                                return View();
                            }





                            var user = new ClaimsPrincipal(
                                new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, loginVM.UserName),
                            new Claim(ClaimTypes.NameIdentifier, roleDetails.FirstOrDefault().SystemUserId.ToString())
                                },
                                CookieAuthenticationDefaults.AuthenticationScheme));
                            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, user, new AuthenticationProperties
                            {
                                ExpiresUtc = DateTime.UtcNow.AddMinutes(120)
                            });
                            long? userId = roleDetails.FirstOrDefault().SystemUserId;

                            HttpContext.Session.SetString("UserData", JsonConvert.SerializeObject(roleDetails.ToList()));
                            HttpContext.Session.SetString("UserInfo", JsonConvert.SerializeObject(_adminlocator.SystemUserBL.GetVM(filter: x => x.IdUser == userId).FirstOrDefault()));
                            ConnectionLog(0, loginVM.UserName, systemUser.IdUser);
                            return RedirectToAction("Index", "AboutUs", new { area = "Admin" });
                        }
                        else
                        {
                            ConnectionLog(-3, loginVM.UserName, systemUser.IdUser);
                            ModelState.AddModelError("Error", "Şifre Hatalıdır!");
                            return View();
                        }


                    }
                    else
                    {

                        ModelState.AddModelError("Error", "Sistemde böyle bir kullanıcı bulunamadı");
                        return View();
                    }

                }
                else
                {
                    ModelState.AddModelError("Error", "Kullanıcı Adı veya Şifre Yanlış!");
                    return View();
                }
            }
            catch (System.Exception ex)
            {

                ModelState.AddModelError("Error", "Beklenmedik Bir Hata Oluştu!");
                return View();
            }

        }
        private void ConnectionLog(short errorCode, string userName, long idUser)
        {

            ConnectionLog cl = new ConnectionLog();
            cl.IdSystemUserRef = idUser;
            cl.ErrorCode = errorCode;
            cl.IpAddress = HttpRequestInfo.IpAddress;
            cl.LogDate = DateTime.Now;
            cl.Username = userName;
            _adminlocator.ConnectionLogBL.CRUD.Insert(cl);
            _adminlocator.ConnectionLogBL.Save();
        }
    }
}