<template>
    <v-layout align-start>
        <v-flex>
            <v-toolbar flat color="white">
                <v-toolbar-title>Roles</v-toolbar-title>
                    <v-divider
                    class="mx-2"
                    inset
                    vertical
                    ></v-divider>
                    <v-spacer></v-spacer>
                    <v-text-field class="text-xs-center" v-model="search" append-icon="search" label="Búsqueda" single-line hide-details></v-text-field>
                    <v-spacer></v-spacer>
                </v-toolbar>
            <v-data-table
                :headers="headers"
                :items="roles"
                :search="search"
                class="elevation-1"
            >
                <template slot="items" slot-scope="props">                    
                    <td>{{ props.item.id }}</td>
                    <td>{{ props.item.nombreRoles }}</td>
                    <td>
                        <div v-if="props.item.condicion">
                            <span class="blue--text">Activo</span>
                        </div>
                        <div v-else>
                            <span class="red--text">Inactivo</span>
                        </div>
                    </td>
                </template>
                <template slot="no-data">
                <v-btn color="primary" @click="listar">Resetear</v-btn>
                </template>
            </v-data-table>
        </v-flex>
    </v-layout>
</template>
<script>
    import axios from 'axios'
    export default {
        data(){
            return {
                roles:[],                
                dialog: false,
                headers: [
                    { text: 'id', value: 'id' },
                    { text: 'nombreRoles', value: 'nombreRoles', sortable: false  },
                   /*  { text: 'Estado', value: 'condicion', sortable: false  }   */              
                ],
                search: ''             
            }
        },
        computed: {
        },

        watch: {
        },

        created () {
            this.listar();
        },
        methods:{
            listar(){
                let me=this;
                axios.get('api/Roles').then(function(response){
                    //console.log(response);
                    me.roles=response.data.data;
                    console.log(data.data);
                }).catch(function(error){
                    console.log(error);
                });
            }
        }        
    }
</script>
