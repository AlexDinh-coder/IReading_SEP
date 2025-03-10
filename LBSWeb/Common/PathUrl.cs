﻿namespace LBSWeb.Common
{
    public class PathUrl
    {
        //Account
        public static string ACCOUNT_REGISTER = "api/Account/Register";
        public static string ACCOUNT_LOGIN = "api/Account/Login";
        public static string ACCOUNT_GETALL = "api/Account/GetAll";
        public static string ACCOUNT_GET_INFO = "api/Account/Information";
        public static string ACCOUNT_UPDATE_INFO = "api/Account/UpdateInformation";
        public static string ACCOUNT_CONFIRMEMAIL = "api/Account/ConfirmEmail";
        public static string ACCOUNT_TOGGLE_LOCK_USER = "api/Account/ToggleLockUser";
        public static string ACCOUNT_ACCESS_ROLE = "api/Account/GrantAccessRole";
        public static string ACCOUNT_FORGOT_PASSWORD = "api/Account/ForgotPassword";
        public static string ACCOUNT_CHANGE_PASSWORD = "api/Account/ChangePassword";
        public static string ACCOUNT_RE_CONFIRM_EMAIL = "api/Account/ReConfirmEmail";
        public static string ACCOUNT_LOGIN_WITH_GOOGLE = "api/Account/LoginWithGoogle";

        //Book
        public static string CATEGORY_GET_ALL = "api/Book/GetCategories";
        public static string CATEGORY_UPDATE = "api/Book/UpdateCategory";
        public static string CATEGORY_DELETE = "api/Book/DeleteCategory";
        public static string BOOK_GET = "api/Book/GetBook";
        public static string BOOK_CREATE = "api/Book/CreateBook";
        public static string BOOK_GET_BY_USER = "api/Book/GetAllBookByUser";
        public static string BOOK_UPDATE_CHAPTER = "api/Book/UpdateBookChapter";
        public static string BOOK_CREATE_CHAPTER = "api/Book/CreateBookChapter";
        public static string BOOK_GET_CHAPTER = "api/Book/GetListBookChapter";
        public static string BOOK_GET_BOOK_CHAPTER = "api/Book/GetBookChapter";
        public static string BOOK_GENERATE_SUMMARY_CHAPTER = "api/Book/GenerateSummary";
        public static string BOOK_GENERATE_POSTER_CHAPTER = "api/Book/GeneratePoster";

                //Information
        public static string INFO_LIST_BASICKNOWLEDGE = "api/Information/BasicKnowledge";
        public static string INFO_DETAIL_BASICKNOWLEDGE = "api/Information/KnowledgeDetail";
        public static string INFO_LIST_NOTIFICATION = "api/Information/ListNotification";
        public static string INFO_DETAIL_NOTIFICATION = "api/Information/NotificationDetail";
        public static string INFO_LIST_CONSPECTUS = "api/Information/ListConspectus";
        public static string INFO_UPDATE_CONSPECTUS = "api/Information/UpdateConspectus";
        public static string INFO_DETAIL_CONSPECTUS = "api/Information/ConspectusDetail";
        public static string INFO_DELETE_CONSPECTUS = "api/Information/DeleteConspectus";
    }

}
