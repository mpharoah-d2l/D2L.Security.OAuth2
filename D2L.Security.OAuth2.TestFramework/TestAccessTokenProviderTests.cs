﻿using System;
using System.Net.Http;
using D2L.Security.OAuth2.Provisioning;
using D2L.Security.OAuth2.Scopes;
using NUnit.Framework;

namespace D2L.Security.OAuth2.TestFramework {

	[TestFixture]
	internal sealed class TestAccessTokenProviderTests {

		[Test]
		public async void TestTestAccessTokenProvider()
		{
			using (var httpClient = new HttpClient())
			{
				IAccessTokenProvider provider = TestAccessTokenProviderFactory.Create(httpClient, "https://auth-dev.proddev.d2l/core" );

				IAccessToken token = await provider.ProvisionAccessTokenAsync( new ClaimSet( "ExpandoClient", Guid.NewGuid() ), new[] { new Scope( "*", "*", "*" ) } );

				Console.WriteLine( token.Token );
			}
			
		}
	}
}
