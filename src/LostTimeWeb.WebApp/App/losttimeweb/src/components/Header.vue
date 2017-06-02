<template>
    <header>
        <nav class="navbar navbar-default ">
            <div class="container-fluid">
                <div class="navbar-header">
                    <a class="navbar-brand" href="#">
                        <img src="/img/logo_noParticle.png" alt="brand"/> LostTime
                    </a>
                </div>
                <ul class="nav navbar-nav navbar-center ">
                    <li><router-link to="/">Home</router-link></li>
                    <li><router-link to="/support">Support</router-link></li>
                    <li><router-link to="/download">Download</router-link></li>
                    <!--<li><router-link to="/forum">Forum</router-link></li>-->
                    <li><router-link to="/usersettings">Profil</router-link></li>
                    <li><router-link to="/login">Login</router-link></li>
                </ul>
                <div class="col-xs-6">user profile/connexion</div>
            </div>
        </nav>
    </header>
</template>
<script>
    export default {
        name: 'header',
    }
</script>
<style lang="less">
.navbar-brand
{
    min-height:150px;
}
</style>