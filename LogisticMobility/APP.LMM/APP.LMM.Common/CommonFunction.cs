using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Web.Mvc;

namespace APP.LMM.Common
{
    public static class CommonFunction
    {
        #region Public Constant
        public const string LMM_Connstring = "LMM_Connstring";
        public const string LMM_DATE_FORMAT_1 = "yyyy-MM-dd";
        public const string LMM_DATE_FORMAT_2 = "dd-MM-yyyy";
        public const string LMM_CULTURE_en_US = "en-US";
        public const string LMM_CULTURE_id_ID = "id-ID";

        public const string ContinueAction = "cont";
        public const string BackAction = "back";
        public const string SaveAction = "save";

        public const string DefaultAdminRole = "Admin";
        public const string DefaultUserRole = "User";
        public const string EmailAvailable = "Email <b>{0}</b> is available";

        public const string EmailNotAvailable = "Email <b>{0}</b> is not available. Please choose another one.";
        public const string XNotAvailable = "{0} <b>{1}</b> is not available. Please choose another one.";

        public const string PasswordCorrect = "Correct password";
        public const string PasswordInCorrect = "Password Incorrect";

        public const string AssignedCategory = "Assigned";
        public const string AvailableCategory = "Available";

        public const string SuccessMessage = "{0} successfully {1}";
        public const string FailMessage = "{0} fail to {1}. {2}";

        public const string DefaultBinType = "RoRo";
        public const string DefaultVehicleType = "Prime Mover";
        public const string DefaultAdhocBinType = "Adhoc";

        public const int AssignPageSize = 10;
        //TODO: Move this method to utitlities
        public static string GetRandomPassword()
        {
            System.Text.StringBuilder newPwd = new System.Text.StringBuilder();
            for (int i = 0; i < 6; i++)
            {
                Random r = new Random();
                System.Threading.Thread.Sleep(10);
                newPwd.Append(r.Next(0, 9));
            }
            return newPwd.ToString();
        }
        #endregion

        #region Private Constant        
        #endregion

        #region Public Method and Properties

        // Summary:
        //     retrieve culture list
        public static IEnumerable<KeyValuePair<string, string>> GetCultureList()
        {
            List<KeyValuePair<string, string>> result = new List<KeyValuePair<string, string>>();
            result.Add(new KeyValuePair<string, string>("id-ID", "Bahasa"));
            result.Add(new KeyValuePair<string, string>("en-US", "English"));
            return result;
        }

        // Summary:
        //     parse string date format to datetime.
        public static DateTime TextDateToDateTime(string value, string format = LMM_DATE_FORMAT_1)
        {
            DateTime result = DateTime.MinValue;
            if (string.IsNullOrEmpty(value))
                return result;

            DateTime.TryParseExact(value, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out result);
            return result;
        }

        // Summary:
        //     retrieve full month name based on cultureinfo.
        public static string GetMonthName(DateTime dateTime, string culture = LMM_CULTURE_en_US)
        {
            string result = dateTime.ToString("MMMM", new CultureInfo(culture));
            return result;
        }

        // Summary:
        //     retrieve error message from ModelState, based on data annotation validation.
        public static string GetModelStateErrorMessage(ICollection<ModelState> collModelState)
        {
            StringBuilder sb = new StringBuilder();
            foreach (ModelState mState in collModelState)
            {
                foreach (ModelError mError in mState.Errors)
                {
                    sb.AppendLine(mError.ErrorMessage);
                }
            }

            return sb.Length > 0 ? sb.ToString() : string.Empty;
        }

        #endregion

        #region Private Method and Properties
        #endregion
    }
}
