<template>
    <header>
        <nav class="navbar navbar-default">
        <div class="container-fluid">
            <div class="navbar-header">
            <a class="navbar-brand" href="#">
                <i class="fa fa-laptop"></i>
                LostTimeWeb
            </a>
            </div>
            <ul class="nav navbar-nav navbar-right">
            <li><router-link to="/">Home</router-link></li>
            <li><router-link to="/support">Support</router-link></li>
            <li><router-link to="/download">Download</router-link></li>
            <li><router-link to="/forum">Forum</router-link></li>
            <li><router-link to="/login">Login</router-link></li>
            </ul>
        </div>
        </nav>
    </header>
</template>
<script>
    export default {
        name: 'header',
    }
</script>
