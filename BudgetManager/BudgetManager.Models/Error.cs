using System;
using System.Collections.Specialized;
using System.ComponentModel.DataAnnotations;
using System.Web;
using System.Xml.Serialization;
using BudgetManager.Models.Base;
using Environment = BudgetManager.Common.Environment;

namespace BudgetManager.Models
{
    /// <summary>
    /// Error object to be used in utilities project
    /// </summary>
    [Serializable]
    [XmlRoot("error")]
    public class Error : IdModelBase
    {
        #region Fields
        private readonly Exception _exception;
        private string _version;
        //private variables for db columns
        private DateTime _errorDate;
        private string _message;
        private string _detail;
        private string _className;
        private string _functionName;
        private string _xml;
        //private variables that will be logged in xml
        private string _hostName;
        private NameValueCollection _serverVariables;
        private NameValueCollection _queryString;
        private NameValueCollection _form;
        private NameValueCollection _cookies; //made it is NameValueCollection for consistency 
        #endregion

        #region Constructors
        public Error(){}
        /// <summary>
        /// Initializes a new instance of the <see cref="Error"/> class.
        /// </summary>
        /// <param name="e">The e.</param>
        /// <param name="applicationName">Name of the application.</param>
        public Error(Exception e): this(e, null){}
        /// <summary>
        /// Initializes a new instance of the <see cref="Error"/> class.
        /// </summary>
        /// <param name="e">The e.</param>
        /// <param name="applicationName">Name of the application.</param>
        /// <param name="context">The context.</param>
        public Error(Exception e, HttpContext context)
        {
            _exception = e;
            Exception baseException = e.GetBaseException();
            _detail = baseException.StackTrace ?? string.Empty;
            _message = e.Message == baseException.Message ? e.Message : baseException.Message + " (" + e.Message + ")";
            _errorDate = DateTime.Now;
            _hostName = Environment.HostName(context);
            _version = Environment.HostVersion();
            if (baseException.TargetSite != null)
            {
                _functionName = baseException.TargetSite.Name;
                if (baseException.TargetSite.DeclaringType != null)
                {
                    _className = baseException.TargetSite.DeclaringType.FullName;
                }
            }
            if (context != null)
            {
                HttpRequest request = context.Request;
                if (request.ServerVariables.Count > 0)
                {
                    _serverVariables = new NameValueCollection(request.ServerVariables);
                }
                if (request.QueryString.Count > 0)
                {
                    _queryString = new NameValueCollection(request.QueryString);
                }
                if (request.Form.Count > 0)
                {
                    _form = new NameValueCollection(request.Form);
                }
                if (request.Cookies.Count > 0)
                {
                    if (_cookies == null)
                    {
                        _cookies = new NameValueCollection();
                    }
                    foreach (HttpCookie cookie in request.Cookies)
                    {
                        _cookies.Add(cookie.Name, cookie.Value);
                    }
                }
            }
            _xml = Encode(this);
        }
        #endregion

        /// <summary>
        /// Gets the exception.
        /// </summary>
        /// <value>The exception.</value>
        public Exception Exception
        {
            get { return _exception; }
        }
        [XmlElement("hostName")]
        [StringLength(128)]
        public string HostName
        {
            get { return _hostName; }
            set { _hostName = value; }
        }
        [XmlElement("hostVersion")]
        [StringLength(128)]
        public string Version
        {
            get { return _version; }
            set { _version = value; }
        }
        /// <summary>
        /// Gets or sets the error date.
        /// </summary>
        /// <value>The error date.</value>
        [XmlElement("errorDate")]
        public DateTime ErrorDate
        {
            get { return _errorDate; }
            set { _errorDate = value; }
        }
        /// <summary>
        /// Gets or sets the error message.
        /// </summary>
        /// <value>The error message.</value>
        [XmlElement("errorMessage")]
        public string ErrorMessage
        {
            get { return _message; }
            set { _message = value; }
        }
        /// <summary>
        /// Gets or sets the error detail (this should be the stack trace).
        /// </summary>
        /// <value>The error detail.</value>
        [XmlElement("errorDetail")]
        [DataType(DataType.MultilineText)]
        public string ErrorDetail
        {
            get { return _detail; }
            set { _detail = value; }
        }
        /// <summary>
        /// Gets or sets the name of the class.
        /// </summary>
        /// <value>The name of the class.</value>
        [XmlElement("className")]
        [StringLength(128)]
        public string ClassName
        {
            get { return _className; }
            set { _className = value; }
        }
        /// <summary>
        /// Gets or sets the name of the function.
        /// </summary>
        /// <value>The name of the function.</value>
        [XmlElement("functionName")]
        [StringLength(128)]
        public string FunctionName
        {
            get { return _functionName; }
            set { _functionName = value; }
        }
        /// <summary>
        /// Gets or sets the error XML.
        /// </summary>
        /// <value>The error XML.</value>
        [DataType(DataType.MultilineText)]
        public string ErrorXml
        {
            get { return _xml; }
            set { _xml = value; }
        }

        #region override
        /// <summary>
        /// Returns a <see cref="System.String"/> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String"/> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return this.ErrorMessage;
        } 
        #endregion

        #region Helpers
        /// <summary>
        /// Encode to error to XML string
        /// </summary>
        /// <param name="e">The e.</param>
        /// <returns></returns>
        public static string Encode(Models.Error e)
        {
            try
            {
                var xmlString = Common.XmlParser.ObjectToXml<Models.Error>(e);
                return xmlString;
            }
            catch
            {
                return "<error>An error occurred trying to encode the error object.</error>";
            }
        } 
        #endregion
    }
}