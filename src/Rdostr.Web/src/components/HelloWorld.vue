<template>
  <div>
    <table style="width:100%">
      <tr>
        <td><img src="@/assets/logov1_200.png"></td>
        <td><h1>Rdost⭐r</h1></td>
        <td style="text-align:right">
          <h2 v-if="user">
            {{user.name}}
          </h2>
          <button @click="login" v-if="!user">Login</button>
          <button @click="logout" v-if="user">Logout</button>
        </td>
      </tr>
    </table>
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
    }
  },
  computed: {
    user: function() {
      return this.$AuthService.getUser();
    }
  }
};
</script>
