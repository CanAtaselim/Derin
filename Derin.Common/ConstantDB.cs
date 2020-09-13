using System;
using System.Collections.Generic;
using System.Text;

namespace Derin.Common
{
    /// <summary>
    /// Database configurations must be defined in this class.
    /// Database Table names must be defined in this class.
    /// Database SP,View,Function etc. names must be defined in this class.
    /// </summary>
    class ConstantDB
    {

        #region ModuleName: GENERAL DATABASE TABLE NAMES
        public const string DB_TABLE_TABLE_VERI = "TD.dbo.TABLE_VERI"; //TEST
        public const string DB_TABLE_DENTADA_ADMINISTRATION_EVENT = "ADMINISTRATION.Event";
        public const string DB_TABLE_DENTADA_ADMINISTRATION_EXCEPTION = "ADMINISTRATION.Exception";
        public const string DB_TABLE_DENTADA_ADMINISTRATION_SYSTEMUSER = "ADMINISTRATION.SystemUser";
        public const string DB_TABLE_DENTADA_ADMINISTRATION_ROLE = "ADMINISTRATION.Role";
        public const string DB_TABLE_DENTADA_ADMINISTRATION_ROLE_PERMISSON = "ADMINISTRATION.RolePermissionDB";
        #endregion

        #region ModuleName: GENERAL DATABASE STOREDPROCEDURE NAMES
        public const string DB_SP_TABLE_VERI_Read_List = "TD.dbo.TABLE_VERI_Read_List"; //TEST
        public const string DB_SP_DENTADA_ADMINISTRATION_ROLE_List = "ADMINISTRATION.Role_List";
        public const string DB_SP_DENTADA_ADMINISTRATION_ROLE__PERMISSON_List = "ADMINISTRATION.RolePermission_List";
        public const string DB_SP_DENTADA_ADMINISTRATION_CDC_Insert = "CDC.CDC_Insert";
        #endregion

        #region ModuleName: GENERAL DATABASE FUNCTION NAMES

        #endregion

        #region ModuleName: GENERAL DATABASE VIEW NAMES

        #endregion
    }
}
