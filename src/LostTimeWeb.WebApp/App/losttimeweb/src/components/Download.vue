<template>
  <div id="download" class="row backgrey">
        <div class="col-md-5 col-md-offset-2">
            <h1>Télécharger la dernière version</h1>
            <p><a href="#"><img src="../../dist/img/en_badge_web_generic.png"/></a></p>
        </div>
        <div class="col-md-4">
            <h2>Changelog V{{versionLog.version}}</h2>
            <ul v-for="i of versionLog.changelog">
                <li>{{i}}</li>
            </ul>
        </div>
</div>
</template>
<script>
    export default {
        data () {
            return {
                versionLog:{version: 'X.X.X', changelog: ['Things changed','Other things changed','You get the point','Srlsy ?']}
            }
        }
    }
</script>
<style lang="less">
#download img
{
    max-width:200px;
    height:auto;
}
</style>