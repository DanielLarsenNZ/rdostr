import * as Msal from 'msal'
import config from '../config'

export default class AuthService {
  constructor () {
    this.applicationConfig = {
      auth: config
    }
    this.app = new Msal.UserAgentApplication(this.applicationConfig)
    this.app.handleRedirectCallback((error, response) => {
      // handle redirect response or error
      console.error(error.errorMessage)
    })
  }

  login () {
    var jwt = this.jwt = ''
    this.app.loginPopup().then(
      token => {
        jwt = token.idToken.rawIdToken
        console.log('JWT token ' + token)
      },
      error => {
        console.log('Login error ' + error)
      }
    )
  }

  logout () {
    this.app._user = null
    this.app.logout()
  }

  getUser () {
    return this.app.getAccount()
  }

  getToken () {
    var jwt = this.jwt
    if (jwt === undefined) {
      // if the user is already logged in you can acquire a token
      if (this.app.getAccount()) {
        var tokenRequest = {
            scopes: [this.applicationConfig.auth.clientId]
            //scopes: ["user.read", "mail.send"]
        }
        this.app.acquireTokenSilent(tokenRequest)
            .then(response => {
                // get access token from response
                // response.accessToken
                jwt = response.accessToken
            })
            .catch(err => {
                // could also check if err instance of InteractionRequiredAuthError if you can import the class.
                if (err.name === "InteractionRequiredAuthError") {
                    return this.app.acquireTokenPopup(tokenRequest)
                        .then(response => {
                            // get access token from response
                            // response.accessToken
                            jwt = response.accessToken.idToken.rawIdToken
                        })
                        .catch(err => {
                            // handle error
                            console.error(err.errorMessage)
                        });
                }
            });
      } else {
          // user is not logged in, you will need to log them in to acquire a token
          login()
      }
    }
  }
}
