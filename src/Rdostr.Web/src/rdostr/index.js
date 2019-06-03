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

      fetch(config.getStationsUrl, options).then(resp => {
        // do something with response
        console.log(resp)
      })
    } catch (err) {
      console.log(err)
    }
  }
}
