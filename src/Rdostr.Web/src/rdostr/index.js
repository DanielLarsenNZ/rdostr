import config from '../config'

export default class RdostrService {
  async getStations(authService) {
    try {
      var response = await authService.getToken()
      console.log("response.accessToken", response.accessToken)

      var headers = new Headers()
      var bearer = "Bearer " + response.accessToken
      headers.append("Authorization", bearer)

      var options = {
        method: "GET",
        headers: headers
      }

      var body = await fetch(config.getStationsUrl, options)
      return body.json()
    } catch (err) {
      console.log(err)
      throw err
    }
  }
  async getConfiguration(authService) {
    try {
      var response = await authService.getToken()
      console.log("response.accessToken", response.accessToken)

      var headers = new Headers()
      var bearer = "Bearer " + response.accessToken
      headers.append("Authorization", bearer)

      var options = {
        method: "GET",
        headers: headers
      }

      var body = await fetch(config.getConfigurationUrl, options)
      return body.json()
    } catch (err) {
      console.log(err)
      throw err
    }
  }
}
