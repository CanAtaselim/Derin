using Derin.Data.Model;


namespace Derin.Business.BusinessLogic.Base
{
    public class SPBaseBL
    {
        private DerinEntities _entityForSP;

        protected DerinEntities EntityForSP
        {
            get { return _entityForSP == null ? _entityForSP = new Derin.Data.Model.DerinEntities(ConnectionStrings.Derin_Prod) : _entityForSP; }
        }
       
    }
}
