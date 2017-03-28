using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NASDMS.Systems
{
    /// <summary>
    /// Dựa vào mã lỗi của winerror.h
    /// Tham khảo:
    /// https://msdn.microsoft.com/en-us/library/windows/desktop/ms681381(v=vs.85).aspx
    /// https://www.rpi.edu/dept/cis/software/g77-mingw32/include/winerror.h
    /// http://www.pinvoke.net/default.aspx/Constants/WINERROR.html
    /// 
    /// Đối với HTTP thì sử dụng https://msdn.microsoft.com/en-us/library/system.net.httpstatuscode(v=vs.110).aspx
    /// Đối với Web.Security thì sử dụng https://msdn.microsoft.com/en-us/library/system.web.security.membershipcreatestatus(v=vs.110).aspx
    /// </summary>
    public enum ErrorCode
    {
        ERROR_SUCCESS = 0,
        ERROR_ALREADY_EXISTS = 183,
        ERROR_NOT_FOUND = 1168
    }
    #region AuditTrail
    public enum CategoryAudit
    {
        NA = 0,
        #region Tài chính kế toán
        DomainObject1,
        DomainObject2,
        DomainObject3,
        #endregion Tài chính kế toán

    }
    public enum ActionAudit
    {
        NA = 0,
        /// <summary>
        /// The created
        /// </summary>
        Created = 1,
        /// <summary>
        /// The updated
        /// </summary>
        Updated = 2,
        /// <summary>
        /// The deleted
        /// </summary>
        Deleted = 3,
        /// <summary>
        /// The login sucess
        /// </summary>
        LoginSucess = 4,
        /// <summary>
        /// The login false
        /// </summary>
        LoginFalse = 5
    }
    #endregion
}
