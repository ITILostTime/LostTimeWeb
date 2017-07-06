<template>
<div id="support" class="row backgrey">
        <div class="col-md-5 col-md-offset-3">
            <h1>Un soucis sur le jeu ?<br />
             Contactez nous !<br />
                <a class="btn btn-default" href="mailto:losttime@intechinfo.fr">Cliquez ici</a>
            </h1>
        </div>
</div>
</template>

<script>
export default {
    data: function () {
        return {
            active: false,
            buttonText: 'Click for technical support',
            likedText: 'Ha ha ha ha...  no.'
        }
    },
    methods: {
        liked: function() {
            this.active = !this.active;
        }
    }
}
</script>
<style lang="less">
#support h1
{
    text-align:center;
    margin:10px auto 10px auto;
}
</style>