<template>
    <v-layout align-start>
        <v-flex>
            <v-toolbar flat color="white">
                <v-toolbar-title>Usuarios</v-toolbar-title>
                    <v-divider
                    class="mx-2"
                    inset
                    vertical
                    ></v-divider>
                    <v-spacer></v-spacer>
                    <v-text-field class="text-xs-center" v-model="search" append-icon="search" label="Búsqueda" single-line hide-details></v-text-field>
                    <v-spacer></v-spacer>
                    <v-dialog v-model="dialog" max-width="500px">
                        <v-btn slot="activator" color="primary" dark class="mb-2">Nuevo</v-btn>
                        <v-card>
                            <v-card-title>
                            <span class="headline">{{ formTitle }}</span>
                            </v-card-title>
                
                            <v-card-text>
                            <v-container grid-list-md>
                                <v-layout wrap>                          
                                <v-flex xs12 sm12 md12>
                                    <v-text-field v-model="nombre" label="nombre"></v-text-field>
                                </v-flex>
                                <v-flex xs12 sm12 md12>
                                    <v-text-field v-model="apellido" label="apellido"></v-text-field>
                                </v-flex>
                                <v-flex xs12 sm12 md12>
                                    <v-text-field v-model="correoElectronico" label="correoElectronico"></v-text-field>
                                </v-flex>
                                <v-flex xs12 sm12 md12>
                                    <v-text-field v-model="contraseña" label="contraseña"></v-text-field>
                                </v-flex>
                                <v-flex xs12 sm12 md12>
                                    <v-text-field v-model="claveSeguridad" label="claveSeguridad"></v-text-field>
                                </v-flex>
                                <v-flex xs12 sm12 md12>
                                    <v-text-field v-model="fechaCreacion" label="fechaCreacion"></v-text-field>
                                </v-flex>
                                <v-flex xs12 sm12 md12>
                                    <v-text-field v-model="ultimoAcceso" label="ultimoAcceso"></v-text-field>
                                </v-flex>
                                <v-flex xs12 sm12 md12 v-show="valida">
                                    <div class="red--text" v-for="v in validaMensaje" :key="v" v-text="v">
                                    </div>
                                </v-flex>
                                </v-layout>
                            </v-container>
                            </v-card-text>
                
                            <v-card-actions>
                                <v-spacer></v-spacer>
                                <v-btn color="blue darken-1" flat @click.native="close">Cancelar</v-btn>
                                <v-btn color="blue darken-1" flat @click.native="guardar">Guardar</v-btn>
                            </v-card-actions>
                        </v-card>
                    </v-dialog>
                </v-toolbar>
            <v-data-table
                :headers="headers"
                :items="usuarios"
                :search="search"
                class="elevation-1"
            >
                <template slot="items" slot-scope="props">
                    <td class="justify-center layout px-0">
                        <v-icon
                        small
                        class="mr-2"
                        @click="editItem(props.item)"
                        >
                        edit
                        </v-icon>
                    </td>
                    <td>{{ props.item.id }}</td>
                    <td>{{ props.item.nombre }}</td>
                    <td>{{ props.item.apellido }}</td>
                    <td>{{ props.item.correoElectronico }}</td>
                   <!--  <td>{{ props.item.contraseña }}</td>
                    <td>{{ props.item.claveSeguridad }}</td> -->
                    <td>{{ props.item.fechaCreacion }}</td>
                    <td>{{ props.item.ultimoAcceso }}</td>
                    <td>{{ props.item.estado }}</td>
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
                usuarios:[],                
                dialog: false,
                headers: [
                    { text: 'Opciones', value: 'opciones', sortable: false }, 
                    { text: 'Id', value: 'id' },
                    { text: 'nombre', value: 'nombre' },    
                    { text: 'apellido', value: 'apellido' },
                    { text: 'correoElectronico', value: 'correoElectronico' },
                  /*   { text: 'contraseña', value: 'contraseña' },
                    { text: 'claveSeguridad', value: 'claveSeguridad' }, */
                    { text: 'fechaCreacion', value: 'fechaCreacion' },
                    { text: 'ultimoAcceso', value: 'ultimoAcceso' },
                    { text: 'estado', value: 'estado' },                     
                ],
                search: '',
                editedIndex: -1,
                id: '',
                nombre: '',
                apellido: '',
                correoElectronico: '',
                contraseña: '',
                contraseña: '',
                claveSeguridad: '',
                fechaCreacion: '',
                ultimoAcceso: '',
                estado: '',
                valida: 0,
                validaMensaje:[],
                adModal: 0,
                adAccion: 0,
                adNombre: '',
                adId: ''             
            }
        },
        computed: {
            formTitle () {
                return this.editedIndex === -1 ? 'Nuevo Usuario' : 'Actualizar Usuario'
            }
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
                axios.get('api/Usuario').then(function(response){
                    //console.log(response);
                    me.usuarios=response.data.data;
                    console.log(data.data);
                }).catch(function(error){
                    console.log(error);
                });
            },
            editItem (item) {
                this.id=item.id;
                this.nombre=item.nombre;
                this.apellido=item.apellido;
                this.correoElectronico=item.correoElectronico;
                this.contraseña=item.contraseña;
                this.editedIndex=1;
                this.dialog = true
            },

            deleteItem (item) {
                const index = this.desserts.indexOf(item)
                confirm('Are you sure you want to delete this item?') && this.desserts.splice(index, 1)
            },

            close () {
                this.dialog = false;
                this.limpiar();
            },
            limpiar(){
                this.id="";
                this.nombreRoles="";
                this.editedIndex=-1;
            },
            guardar () {
                if (this.validar()){
                    return;
                }
                if (this.editedIndex > -1) {
                    //Código para editar
                    //Código para guardar
                    let me=this;
                    axios.put(`api/Roles?id=${this.id}`,{
                        'id':me.id, 
                        'nombreRoles': me.nombreRoles,
                    }).then(function(response){
                        me.close();
                        me.listar();
                        me.limpiar();                        
                    }).catch(function(error){
                        console.log(error);
                    });
                } else {
                    //Código para guardar
                    let me=this;
                    axios.post('api/Roles',{
                        'nombreRoles': me.nombreRoles,
                    }).then(function(response){
                        me.close();
                        me.listar();
                        me.limpiar();                        
                    }).catch(function(error){
                        console.log(error);
                    });
                }
            },
            validar(){
                this.valida=0;
                this.validaMensaje=[];

                if (this.nombreRoles.length<3 || this.nombreRoles.length>50){
                    this.validaMensaje.push("El nombre debe tener más de 3 caracteres y menos de 50 caracteres");
                }
                if (this.validaMensaje.length){
                    this.valida=1;
                }
                return this.valida;
            },
            activarDesactivarMostrar(accion,item){
                this.adModal=1;
                this.adNombre=item.nombreRoles;
                this.adId=item.id;                
                if (accion==1){
                    this.adAccion=1;
                }
                else if (accion==2){
                    this.adAccion=2;
                }
                else{
                    this.adModal=0;
                }
            },
            activarDesactivarCerrar(){
                this.adModal=0;
            },
        }        
    }
</script>
