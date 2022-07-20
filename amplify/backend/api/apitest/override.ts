// This file is used to override the REST API resources configuration
import { AmplifyApiRestResourceStackTemplate } from '@aws-amplify/cli-extensibility-helper';
import env from './env.config'

export function override(resources: AmplifyApiRestResourceStackTemplate) {
  // Add a parameter to your Cloud Formation Template for the User Pool's ID
  resources.addCfnParameter({
      type: "String",
      description: "The id of an existing User Pool to connect. If this is changed, a user pool will not be created for you.",
      default: "NONE",
    },
    "AuthCognitoUserPoolId",
    { "Fn::GetAtt": ["authvuejsamplifyapitestea5b464b", "Outputs.UserPoolId"], }
  );

  const AWS_REGION = env.AWS_REGION;
  const AWS_ACCOUNT_ID = env.AWS_ACCOUNT_ID;
  const AWS_USER_POOLS_ID = env.AWS_USER_POOLS_ID;

  // Create the authorizer using the AuthCognitoUserPoolId parameter defined above
  resources.restApi.addPropertyOverride("Body.securityDefinitions", {
    Cognito: {
      type: "apiKey",
      name: "Authorization",
      in: "header",
      "x-amazon-apigateway-authtype": "cognito_user_pools",
      "x-amazon-apigateway-authorizer": {
        type: "cognito_user_pools",
        providerARNs: [
          { 'Fn::Sub': `arn:aws:cognito-idp:${AWS_REGION}:${AWS_ACCOUNT_ID}:userpool/${AWS_USER_POOLS_ID}` },
        ],
      },
    },
  });

  // For every path in our REST API
  for (const path in resources.restApi.body.paths) {
    // Add the Authorization header as a parameter to requests
    resources.restApi.addPropertyOverride(
      `Body.paths.${path}.x-amazon-apigateway-any-method.parameters`,
      [
        ...resources.restApi.body.paths[path]["x-amazon-apigateway-any-method"]
          .parameters,
        {
          name: "Authorization",
          in: "header",
          required: true,
          type: "string",
        },
      ]
    );
    // Use our new Cognito User Pool authorizer for security
    resources.restApi.addPropertyOverride(
      `Body.paths.${path}.x-amazon-apigateway-any-method.security`,
      [ { Cognito: [], }, ]
    );
  }
}
