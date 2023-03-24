<template>
    <v-layout align-start>
        <v-flex>
            <v-container fluid>
                <v-card>
                    <v-card-title style="font-weight: 700;" >Bombero Destacado</v-card-title>
                    <v-card-text>
                        <p><span style="font-weight: 700;">Cargo:</span> {{ BomberosDestacado.bomberoCargo }}</p>
                        <p><span style="font-weight: 700;">Cantidad de Incendios:</span> {{ BomberosDestacado.cantidad_de_Incendio }}</p>
                    </v-card-text>
                </v-card>
            </v-container>
        </v-flex>
    </v-layout>
</template>
<script>
    import axios from 'axios'
    export default {
        data(){
            return {
                BomberosDestacado:{},                
                dialog: false, 
                bomberoCargo:'',  
                cantidad_de_Incendio:0,          
            }
        },
        computed: {
            
        },

        watch: {
            dialog (val) {
            val || this.close()
            }
        },

        created () {
            this.listar();
        },
        methods:{
            listar(){
                let me=this;
                axios.get('api/RegistroIncendios/GetBomberoDestacadoAsync').then(function(response){
                    //console.log(response);
                    me.BomberosDestacado=response.data.data;
                }).catch(function(error){
                    console.log(error);
                });
            },

        }        
    }
</script>