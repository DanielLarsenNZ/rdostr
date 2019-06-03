<template>
  <div>
    <div id="label">Sign-in with Microsoft Azure AD B2C</div>

    <button @click="login" v-if="!user">Login</button>
    <button @click="logout" v-if="user">Logout</button>
    <button @click="stations" v-if="user">Stations</button>

    <div v-if="user">Hello from Vue.js. User is {{user.name}}</div>
  </div>
</template>

<script>
export default {
  name: "HelloWorld",
  methods: {
    login() {
      this.$AuthService.login();
    },
    logout() {
      this.$AuthService.logout();
    },
    stations() {
      var tokenRequest = {
        scopes: ["49df1c13-8de7-46fc-a1d9-7bf0c8b73377"]
      };

      this.$AuthService.app
        .acquireTokenSilent(tokenRequest)
        .then(response => {
          // get access token from response
          // response.accessToken
          console.log("response.accessToken", response.accessToken);
          var headers = new Headers();
          var bearer = "Bearer " + response.accessToken;
          headers.append("Authorization", bearer);

          var options = {
            method: "GET",
            headers: headers
          };

          var endpoint = "https://localhost:44369/api/stations";

          fetch(endpoint, options).then(resp => {
            // do something with response
            console.log(resp);
          });
        })
        .catch(err => {
          console.error(err.error);

          // could also check if err instance of InteractionRequiredAuthError if you can import the class.
          if (err.name === "InteractionRequiredAuthError") {
            console.log(
              "InteractionRequiredAuthError, trying acquireTokenPopup"
            );

            return this.$AuthService.app
              .acquireTokenPopup(tokenRequest)
              .then(response => {
                var headers = new Headers();
                var bearer = "Bearer " + response.accessToken;
                headers.append("Authorization", bearer);

                var options = {
                  method: "GET",
                  headers: headers
                };

                var endpoint = "https://localhost:44369/api/stations";

                fetch(endpoint, options).then(resp => {
                  // do something with response
                  console.log(resp);
                });
              })
              .catch(err => {
                // handle error
                console.error(err);
              });
          }
        });
    }
  },
  computed: {
    user: function() {
      return this.$AuthService.getUser();
    }
  }
};
</script>
