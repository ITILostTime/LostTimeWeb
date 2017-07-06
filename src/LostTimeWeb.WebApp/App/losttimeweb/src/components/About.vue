<template>
    <div id="about" class="row backgrey">
        <div class="col-md-4 col-md-offset-2">
            <h1>A-Propos du projet</h1>
            <p>Lost Time est un jeu vidéo dans lequel vous suivez Astrid à travers diverses péripéties et l’aider à sauver le temps. Venez en aide aux habitants en explorant les différents quartiers de la ville : du quartier de la machine au zeppelin surplombant la cité. </p>
            <p>Jouez en coopération avec vos amis et créez vos propres aventures. Résolvez les énigmes en discutant avec vos amis et la communauté grâce aux chat intégré au jeu et à son site.</p>
            <p>Soyez maître de votre destin !</p>
            <h2>Contact</h2>
            <p><a href="mailto:lostime@intechinfo.fr">Nous contacter</a></p>
        </div>
        <div class="col-md-5">
            <h2>Réalisation</h2>
            <div class="bugdroids" >
                <div class="character" v-for="(i, index) in devList" :key="index">
                    <img :src="'/dist/'+i.pictureName" alt="screen"/>
                    <p>{{i.name}}</p>
                </div>
            </div>
            <p class="intech">Réalisé dans le cadre de nos études à IN'TECH, Semestre 5 <br/>(premier semestre 2017) <br/>
            <img src="../../dist/img/Logo_Intech_RVB.jpg" ></p>            
        </div>
    </div>
</template>
<script>
export default {
    data () {
        return {
            devList: [
                { name: 'Guillaume Elisabeth', pictureName: 'GuillaumeR.png' },
                { name: 'Julie Lacognata', pictureName: 'JulieR.png' },
                { name: 'Yacine Chergui', pictureName: 'YacineR.png' },
                { name: 'Pierre Koré', pictureName: 'PierreR.png' },
                { name: 'Quentin Vanbutsele', pictureName: 'QuentinR.png' },
            ]
        }
    }
}
</script>
<style lang="less">
.bugdroids
{
    display:flex;
    flex-wrap: wrap;
    justify-content:center;
}
.character
{
    color:white;
    font-size:1.2em;
    text-align:center;
    max-width:100px;
    margin:10px;
}
.character img
{
    width:100px;
    height:auto;
    margin:10px;
    
}
.intech 
{
    text-align:center;
}
.intech img
{
    width:250px;
    height:auto;
    margin:10px auto 10px auto;
}
</style>