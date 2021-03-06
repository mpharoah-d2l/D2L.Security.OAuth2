﻿using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web.Script.Serialization;
using D2L.Security.OAuth2.Scopes;

namespace D2L.Security.OAuth2.Provisioning {

	internal static class TokenCacheKeyBuilder {

		private static readonly JavaScriptSerializer m_serializer = new JavaScriptSerializer();

		internal static string BuildKey(
			IEnumerable<Claim> claims,
			IEnumerable<Scope> scopes
			) {

			// Sort the claims and scopes before serializing them into a key so that the 
			// cache can be better utilized for token provision requests which have the 
			// same claims and scopes but in different order
			IOrderedEnumerable<Claim> sortedClaims = claims.OrderBy( c => c.Type );
			IOrderedEnumerable<Scope> sortedScopes = scopes.OrderBy( s => s.ToString() );

			var keyObject = new {
				claims = sortedClaims.Select( c => new { name = c.Type, value = c.Value } ),
				scopes = sortedScopes.Select( s => s.ToString() )
			};

			// All the claims and scopes must be used in the key to ensure that two 
			// tokens with different claims or scopes never map to the same key
			return m_serializer.Serialize( keyObject );
		}
	}
}
