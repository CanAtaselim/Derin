using System;
using System.Collections.Generic;
using System.Text;

namespace Derin.Common
{
    class ConstantVarible
    {
        #region ModuleName: General Constant Variables

        public const string _ProjectName = "$ext_safeprojectname$";
        public const string _ProjectLongName = "Project Long Name.";
        public const string _ProjectDescription = "Project desciption.";
        public const string _ProjectAuthor = "SinerjiSoft";
        public const string _ProjectVersion = "0.0.1";
        public const string _ProjectTheme_Theme1 = "~/Views/Metronic_v4.7/Admin1/Layout.cshtml";
        public const string _ProjectTheme_Theme2 = "~/Views/Metronic_v4.7/Admin2/Layout.cshtml";
        public const string _ProjectTheme_Theme3 = "~/Views/Metronic_v4.7/Admin3/Layout.cshtml";
        public const string _ProjectTheme_Theme4 = "~/Views/Metronic_v4.7/Admin4/Layout.cshtml";
        public const string _ProjectTheme_Theme5 = "~/Views/Metronic_v4.7/Admin5/Layout.cshtml";
        public const string _ProjectTheme_Theme6 = "~/Views/Metronic_v4.7/Admin6/Layout.cshtml";
        public const string _ProjectTheme_Current = _ProjectTheme_Theme4;
        public const int _ProjectTheme_LayoutType = (int)_ThemeLayoutType.Default;

        public const bool _Project_Settings_EventLoggingEnabled = true;
        public const bool _Project_Settings_CDCLoggingEnabled = true;
        public const bool _Projec_Settings_MemoryCacheEnabled = true;

        #endregion
    }

    public enum _ThemeLayoutType : int
    {
        Default = 1,
        Material = 2,
        Rounded = 3
    }
}
