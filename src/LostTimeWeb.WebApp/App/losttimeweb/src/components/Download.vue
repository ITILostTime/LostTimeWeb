<template>
  <div id="download" class="row">
        <div class="col-md-5 col-md-offset-3">
            <h1>Télécharger la dernière version</h1>
            <p><a href="#"><img src="img/en_badge_web_generic.png"/></a></p>
        </div>
        <div class="col-md-4">
            <h2>Changelog V{{version}}</h2>
            <ul>
                <li>Things changed</li>
                <li>Other things changed</li>
                <li>You get the point</li>
                <li>Srlsy ?</li>
                <li>...</li>
            </ul>
        </div>
</div>
</template>
<script>
    export default {
        data () {
            return {
                version: 'X.X.X'
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