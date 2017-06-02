<template>
    <section class="carrousel col-md-8">
        <slider animation="fade"
            width="100%"
            height="634px"
            :init-index="0"
            >
            <slider-item v-for="(i, index) in itemList" :key="index">
                <article
                    <img :src="'img/'+i.pictureName" alt="screen"/>
                    <h1>{{i.title}}</h1>
                </article>
            </slider-item>
        </slider>
        
    </section>
</template>

<script>
import { Slider, SliderItem } from 'vue-easy-slider'

    export default {
        data () {
            return {
                itemList: [
                    { title: 'LostTime c\'est cool !', pictureName: 'screen1.png' },
                    { title: 'LostTime c\'est super !', pictureName: 'screen2.png' },
                    { title: 'LostTime c\'est génial !', pictureName: 'screen3.png' },
                    { title: 'LostTime c\'est top moumoute !', pictureName: 'screen4.png' },
                ]
            }
        },
        components: {
            Slider,
            SliderItem
            },
        mounted () {           
            setTimeout(() => 
                this.itemList
            , 1000)
        },
        methods: {
            test () {
                console.log(1)
            },
        },
    }
</script>
<style lang="less">
.slider
{
    height:634px;
    max-height:634px;
    max-width:891px;
}
.carrousel article
{
    position:relative;
    height:634px;
    max-height:634px;
    max-width:891px;
}
.carrousel img
{
    width: 100%; 
    position:relative;
    z-index:0;
}
.carrousel h1
{
    color:white;
    padding:10px;
    padding-bottom:25px;
    margin:0px;
    position:absolute;
    bottom:0px;
    z-index:1;
    width:100%;
    background: -moz-linear-gradient(top, rgba(0,0,0,0) 0%, rgba(0,0,0,0.6) 77%, rgba(0,0,0,0.6) 100%); /* FF3.6-15 */
    background: -webkit-linear-gradient(top, rgba(0,0,0,0) 0%,rgba(0,0,0,0.6) 77%,rgba(0,0,0,0.6) 100%); /* Chrome10-25,Safari5.1-6 */
    background: linear-gradient(to bottom, rgba(0,0,0,0) 0%,rgba(0,0,0,0.6) 77%,rgba(0,0,0,0.6) 100%); /* W3C, IE10+, FF16+, Chrome26+, Opera12+, Safari7+ */
    filter: progid:DXImageTransform.Microsoft.gradient( startColorstr='#00000000', endColorstr='#99000000',GradientType=0 ); /* IE6-9 */
}
</style>