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
    this.app.loginPopup().then(
      token => {
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
}
