<template>
<div id="support" class="row backgrey">
        <div class="col-md-5 col-md-offset-3">
            <h1>
                <button class="like-button" :class="{ 'active': active }" @click="liked">{{ active ? likedText : buttonText }}</button>
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