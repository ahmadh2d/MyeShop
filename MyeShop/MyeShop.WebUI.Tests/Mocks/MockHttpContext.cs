using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace MyeShop.WebUI.Tests.Mocks
{
	public class MockHttpContext : HttpContextBase
	{
		private MockHttpRequest request;
		private MockHttpResponse response;
		private HttpCookieCollection cookies;
		private IPrincipal FakeUser;

		public MockHttpContext()
		{
			cookies = new HttpCookieCollection();
			request = new MockHttpRequest(cookies);
			response = new MockHttpResponse(cookies);
		}

		public override HttpRequestBase Request
		{
			get
			{
				return request;
			}
		}

		public override IPrincipal User 
		{
			get
			{
				return FakeUser;
			}

			set
			{
				FakeUser = value;
			}
		}

		public override HttpResponseBase Response
		{
			get
			{
				return response;
			}
		}
	}

	public class MockHttpResponse : HttpResponseBase
	{
		private readonly HttpCookieCollection cookies;

		public MockHttpResponse(HttpCookieCollection cookies)
		{
			this.cookies = cookies;
		}

		public override HttpCookieCollection Cookies
		{
			get
			{
				return cookies;
			}
		}
	}

	public class MockHttpRequest : HttpRequestBase
	{
		private readonly HttpCookieCollection cookies;

		public MockHttpRequest(HttpCookieCollection cookies)
		{
			this.cookies = cookies;
		}

		public override HttpCookieCollection Cookies
		{
			get
			{
				return cookies;
			}
		}
	}
}
