using System;
using System.ComponentModel;
using System.Reflection;

namespace Derin.Common
{
    public class _Enumeration
    {

        #region Framework Enumerations
        public enum _TypeException : int
        {
            Common = 0,
            DataAccess = Common | 1,
            Service = Common | 2,
            SecurityException = Common | 3,
            Validation = Common | 4,
            Cache = Common | 5,
            UserInterface = Common | 6
        }
        public enum _TypeStatus
        {
            [DescriptionAttribute("Aktif")]
            Active = 10,
            [DescriptionAttribute("Pasif")]
            Passive = 20,
        }
        public enum _TypeProcess
        {
            Insert = 1,
            Update = 2,
            Delete = 3,
            Select = 4
        }
        public enum _TypeEventCode
        {
            [DescriptionAttribute("ExecutionDb")]
            DbExecution = 10,
            [DescriptionAttribute("XXX")]
            ExternalServiceExecution = 20
        }
        public enum _TypeOperationIsDeleted
        {
            [DescriptionAttribute("Kayıt Aktif")]
            UnDeleted = 1,
            [DescriptionAttribute("Kayıt Silindi")]
            Deleted = 2
        }
        public enum _TypeCDCOperation
        {
            [DescriptionAttribute("UPDATE")]
            Update = 1,
            [DescriptionAttribute("DELETE")]
            Delete = 2,
            [DescriptionAttribute("DROP")]
            Drop = 3
        }
        public enum IsOperationDeleted
        {
            [Description("Artık Kullanılmıyor")]
            Cancelled = 3,
            [Description("Silinmiş")]
            Deleted = 2,
            [Description("Aktif")]
            Active = 1,
            [Description("Durumu Bilinmiyor")]
            Empty = 0
        };

        public enum _TypeLayout
        {
            Default = 1,
            MaterialDesign = 2,
            Rounded = 3
        }
        public enum _EmployeeType
        {
            [Description("Yöneticilerimiz")]
            Managers = 1,
            [Description("Medikal Kadro")]
            MedicalStaff = 2,
            [Description("Takım Arkadaşlarımız")]
            OurTeam = 3

        }
        #endregion

        #region Administration Enums

        public enum _AnnouncementPriority
        {
            [DescriptionAttribute("Düşük")]
            Low = 1,
            [DescriptionAttribute("Normal")]
            Medium = 2,
            [DescriptionAttribute("Yüksek")]
            High = 3
        }

        public enum _TypeSystemUserGender
        {
            [DescriptionAttribute("ERKEK")]
            MALE = 1,
            [DescriptionAttribute("KADIN")]
            FEMALE = 2
        }

        public enum _TypeSystemUserEmailConfirmation
        {
            [DescriptionAttribute("ONAYLANDI")]
            ACCEPTED = 1,
            [DescriptionAttribute("ONAY BEKLİYOR")]
            WAITING = 2
        }
        public enum _TypeSystemUserStatus
        {
            [DescriptionAttribute("AKTİF")]
            ACTIVE = 1,
            [DescriptionAttribute("PASİF")]
            AWAITING = 2,
            [DescriptionAttribute("YASAKLI")]
            BANNED = 3
        }
        #endregion
        public static string GetEnumDescription(Enum value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());

            DescriptionAttribute[] attributes =
                (DescriptionAttribute[])fi.GetCustomAttributes(
                typeof(DescriptionAttribute),
                false);

            if (attributes != null &&
                attributes.Length > 0)
                return attributes[0].Description;
            else
                return value.ToString();
        }

    }


}
