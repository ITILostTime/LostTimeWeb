<template>
    <header>
        <nav class="navbar navbar-default " id="head">
            <div class="container-fluid">
                <div class="navbar-header">
                    <router-link to="/">
                        <img src="../../../dist/img/logo_noParticle.png" alt="brand"/> 
                        LostTime
                    </router-link>
                </div>
                <ul class="nav navbar-nav navbar-center ">
                    <li><router-link to="/">Accueil</router-link></li>
                    <li><router-link to="/download">Téléchargement</router-link></li>                    
                    <li><router-link to="/support">Support technique</router-link></li>
                    <!--<li><router-link to="/forum">Forum</router-link></li>-->
                </ul>
                <div class="collapse navbar-collapse"  v-if="auth.isConnected"><!--IF CONNECTED -->
                    <ul class="nav navbar-nav">
                        <li><router-link :to="`/user/${auth.id}`">Profil</router-link></li>
                        <li><router-link to="/tchat">Tchat</router-link></li>
                        <li><router-link to="/admin">Administration</router-link></li>
                    </ul>
                    <ul class="nav navbar-nav navbar-right">
                        <li class="dropdown">
                        <a href="#" class="dropdown-toggle" data-toggle="dropdown" role="button" aria-haspopup="true" aria-expanded="false">{{ auth.email }} <span class="caret"></span></a>
                        <ul class="dropdown-menu">
                            <li><router-link to="/logout">Se déconnecter</router-link></li>
                        </ul>
                        </li>
                    </ul>
                </div>
                <div class="collapse navbar-collapse"  v-else><!-- IF NOT CONNECTED -->
                    <ul class="nav navbar-nav">
                        <li><router-link to="/login">Se connecter</router-link></li>
                    </ul>
                </div>
            </div> <!--div contaier fluid-->
        </nav>
    </header>
</template>
<script>
import AuthService from '../../services/AuthService'
import { mapGetters, mapActions } from 'vuex'
import '../../directives/requiredProviders'

export default {
    name: 'header',
    computed: {
    auth: () => AuthService,
    //...mapGetters(['isLoading'])
  }
}
</script>
<style lang="less">

.navbar
{
    background-color:grey;
    border:darkgrey;
    max-height:50px;
    font-color:black;
}
#head a
{
  color:white;
}
#head img
{
     max-height:150px;

}
#head .navbar-header
{
    position:absolute;
    z-index:2000;
    background-color:#9f9080;
    width:300px; 
    //border: 2px solid;
    border-bottom-left-radius: 2em;
    border-bottom-right-radius: 2em;
    padding:10px;
    box-shadow:2px 2px 6px black;
}
#head .navbar-header a
{
    font-family:"The Goldsmith Vintage", "Impact", serif;
    font-size:3em;
    text-decoration:none;
    color:#493836;
    background: -webkit-linear-gradient(#c6823c, #483e2f);
    -webkit-background-clip: text;
    -webkit-text-fill-color: transparent;
}
.navbar-center
{
    margin-left:310px;
}
</style>