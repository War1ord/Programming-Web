using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using BudgetManager.Business.Error;
using BudgetManager.Common;
using BudgetManager.Common.Messages;
using BudgetManager.Extentions;
using BudgetManager.Models.User;
using BudgetManager.Web.Models;

namespace BudgetManager.Web.Base
{
    /// <summary>
    /// The Base Controller
    /// </summary>
    public abstract class BaseController : Controller
    {
        #region Fields

        /// <summary>
        ///     The session key
        /// </summary>
        private const string SessionKey = "36E47B3C-0BA4-4084-BFCE-A4504798F21B";

        /// <summary>
        ///     Gets or sets the current URL.
        /// </summary>
        /// <value>
        ///     The current URL.
        /// </value>
        public Uri CurrentUrl { get; set; }

        #endregion

        #region Properties

        /// <summary>
        ///     Gets a value indicating whether the current user is admin.
        /// </summary>
        /// <value>
        ///     <c>true</c> if this instance is admin; otherwise, <c>false</c>.
        /// </value>
        public bool IsAdmin
        {
            get { return CurrentUser.IsAdmin; }
        }

        /// <summary>
        ///     Gets the current user.
        /// </summary>
        /// <value>
        ///     The current user.
        /// </value>
        public User CurrentUser
        {
            get { return session != null ? session.User : null; }
        }

        public Uri ReturnUrl
        {
            get { return session != null ? session.ReturnUrl : null; }
        }

        /// <summary>
        ///     Gets or sets the session.
        /// </summary>
        /// <value>
        ///     The session.
        /// </value>
        public Session session { get; set; }

        #endregion

        #region Overrides

        /// <summary>
        ///     Initializes data that might not be available when the constructor is called.
        /// </summary>
        /// <param name="requestContext">The HTTP context and route data.</param>
        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);
            ActionSession(requestContext.HttpContext);
            CurrentUrl = GetCurrentUrl(requestContext.HttpContext);
        }

        /// <summary>
        ///     Called when authorization occurs.
        /// </summary>
        /// <param name="filterContext">Information about the current request and action.</param>
        protected override void OnAuthorization(AuthorizationContext filterContext)
        {
            base.OnAuthorization(filterContext);
            if (!IsUserAuthorized() && NotLoginAction() && NotRegisterAction() && NotValidationAction())
            {
                filterContext.Result = RedirectToLoginWithReturnUrl();
            }
        }

        /// <summary>
        ///     Called after the action method is invoked.
        /// </summary>
        /// <param name="filterContext">Information about the current request and action.</param>
        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            base.OnActionExecuted(filterContext);
            ActionSession(filterContext.HttpContext);
            if (filterContext.HttpContext.Request.HttpMethod != "POST")
            {
                session.ReturnUrl = GetCurrentUrl(filterContext.HttpContext);
            }
        }

        #endregion

        #region Protected Helpers

        /// <summary>
        ///     Determines whether the specified user id is authorized.
        /// </summary>
        /// <param name="userId">The user id.</param>
        /// <returns>
        ///     <c>true</c> if the specified user id is authorized; otherwise, <c>false</c>.
        /// </returns>
        public bool IsAuthorizedUserId(Guid userId)
        {
            return userId == CurrentUser.Id && CurrentUser.IsAdmin;
        }

        /// <summary>
        ///     Determines whether [is user authorized] [the specified user].
        /// </summary>
        /// <returns>
        ///     <c>true</c> if [is user authorized] [the specified user]; otherwise, <c>false</c>.
        /// </returns>
        public bool IsUserAuthorized()
        {
            return IsUserValid()
                   && IsUserValidated()
                   && IsUserAuthenticated()
                   && !IsUserLocked();
        }

        /// <summary>
        ///     Determines whether user is valid.
        /// </summary>
        /// <returns>
        ///     <c>true</c> if user is valid; otherwise, <c>false</c>.
        /// </returns>
        public bool IsUserValid()
        {
            return session.User != null
                   && session.User.Id != Guid.Empty;
        }

        /// <summary>
        ///     Determines whether user is validated.
        /// </summary>
        /// <returns>
        ///     <c>true</c> if user is validated; otherwise, <c>false</c>.
        /// </returns>
        public bool IsUserValidated()
        {
            return IsUserValid()
                   && session.User.IsValidated;
        }

        /// <summary>
        ///     Determines whether user is authenticated.
        /// </summary>
        /// <returns>
        ///     <c>true</c> if user is authenticated; otherwise, <c>false</c>.
        /// </returns>
        public bool IsUserAuthenticated()
        {
            return IsUserValid()
                   && session.User.IsAuthenticated;
        }

        /// <summary>
        ///     Determines whether user is locked.
        /// </summary>
        /// <returns>
        ///     <c>true</c> if user is locked; otherwise, <c>false</c>.
        /// </returns>
        public bool IsUserLocked()
        {
            return IsUserValid()
                   && session.User.IsLocked;
        }

        /// <summary>
        ///     Redirects to return Return URL.
        /// </summary>
        /// <returns></returns>
        protected RedirectResult RedirectToReturnUrl()
        {
            return Redirect(Request.QueryString["returlUrl"] ?? "/");
        }

        /// <summary>
        ///     Redirects to login with return URL.
        /// </summary>
        /// <returns></returns>
        protected RedirectToRouteResult RedirectToLoginWithReturnUrl()
        {
            return RedirectToAction("Login", "Account", new {returlUrl = CurrentUrl.PathAndQuery});
        }

        /// <summary>
        ///     Logs the exception.
        /// </summary>
        /// <param name="exception">The e.</param>
        public string LogException(Exception exception)
        {
            ErrorManager.LogException(exception);
            return CommonMessages.UnexpectedError;
        }

        /// <summary>
        ///     Shows Errors or Messages from business with out a viewmodel or custom view. Business Managers values are indected
        ///     to an anonymous object.
        /// </summary>
        /// <param name="manager">The manager.</param>
        /// <param name="fallBackMessage">The fall back message.</param>
        /// <returns></returns>
        protected ActionResult ShowErrorOrMessage(Business.Base.BusinessBase manager, string fallBackMessage = "")
        {
            var model = new
            {
                returnUrl = ReturnUrl.PathAndQuery,
                manager.Result.Message,
                manager.Result.Type,
                fallBackMessage
            };
            return View("Message", model.ToExpando());
        }

        #region Convert Helpers

        protected string ToMultilineString(ValidationResult validationResult)
        {
            return validationResult.ValidationErrors.Select(i => i.Value).Concatenate();
        }

        protected string ToMultilineString(IEnumerable<string> strings)
        {
            return strings.Concatenate();
        }

        #endregion

        #endregion

        #region Private Helpers

        /// <summary>
        ///     Actions the session.
        /// </summary>
        /// <param name="requestContext">The request context.</param>
        private void ActionSession(HttpContextBase httpContext)
        {
            if (httpContext.Session != null)
            {
                session = httpContext.Session[SessionKey] as Session;
                if (session == null)
                {
                    httpContext.Session[SessionKey] = session = new Session();
                }
            }
        }

        private bool NotValidationAction()
        {
            return Url.Action("Validate", "Account") != CurrentUrl.AbsolutePath;
        }

        /// <summary>
        ///     Checks if the current action is not the register action.
        /// </summary>
        /// <returns></returns>
        private bool NotRegisterAction()
        {
            return Url.Action("Register", "Account") != CurrentUrl.AbsolutePath;
        }

        /// <summary>
        ///     Checks if the current action is not the Login action.
        /// </summary>
        /// <returns></returns>
        private bool NotLoginAction()
        {
            return Url.Action("Login", "Account") != CurrentUrl.AbsolutePath;
        }

        /// <summary>
        ///     Gets the current URL.
        /// </summary>
        /// <param name="httpContext">The HTTP context.</param>
        /// <returns></returns>
        private Uri GetCurrentUrl(HttpContextBase httpContext)
        {
            return httpContext.Request.Url;
        }

        #endregion
    }
}