using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using System.Threading.Tasks;

namespace AspCoreSso.Middlewares
{
    /// <summary>
    /// HTTPヘッダー認証Middleware
    /// </summary>
    public class HeaderAuthentication
    {
        private readonly RequestDelegate _next;

        public HeaderAuthentication(RequestDelegate next)
        {
            _next = next;
        }

        /// <summary>
        /// HTTPヘッダーのユーザーがシステムに存在するか検証します。
        /// </summary>
        /// <param name="context">HttpContext</param>
        /// <returns>なし（次のMiddlewareに処理を委譲する）</returns>
        public async Task Invoke(HttpContext context)
        {
            var isAuthenticated = false;

            context.Request.Headers.TryGetValue("USERNAME", out StringValues username);
            if (!StringValues.IsNullOrEmpty(username))
            {
                if (username.ToString().Equals("user1"))
                {
                    isAuthenticated = true;
                }
            }

            if (isAuthenticated)
            {
                // 処理を委譲
                await _next(context);
            }
            else
            {
                // 403を出力して処理を終了（委譲しない）
                context.Response.StatusCode = 403;
            }
        }
    }
}
