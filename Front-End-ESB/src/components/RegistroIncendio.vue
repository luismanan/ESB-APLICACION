<template>
    <v-layout align-start>
        <v-flex>
            <v-toolbar flat color="white">
                <v-toolbar-title>Registro de Incendios</v-toolbar-title>
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
                                    <v-text-field v-model="direccion" label="Direccion"></v-text-field>
                                </v-flex>
                                <v-flex xs12 sm12 md12>
                                        <v-select v-model="idBomberoCargo" :items="clientes" label="Bombero a Cargo">
                                        </v-select>
                                </v-flex>
                             <!--    <v-flex xs12 sm12 md12>
                                    <v-text-field type="date" v-model="fechaCreacion" label="FechaCreacion"></v-text-field>
                                </v-flex> -->
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
                :items="RegistroIncendio"
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
                    <td>{{ props.item.direccion }}</td>
                    <td>{{ props.item.bomberoCargo }}</td>
                    <td>{{ props.item.fechaCreacion }}</td>
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
                RegistroIncendio:[],                
                dialog: false,
                headers: [
                    { text: 'Opciones', value: 'opciones', sortable: false },
                    { text: 'Id', value: 'id', sortable: false },
                    { text: 'Direccion', value: 'direccion' },
                    { text: 'BomberoCargo', value: 'bomberoCargo' },
                    { text: 'FechaCreacion', value: 'fechaCreacion' },
                          
                ],
                search: '',
                editedIndex: -1,
                id: '',
                direccion: '',
                bomberoCargo: '',
                idBomberoCargo:'',
                clientes:[],
                fechaCreacion: '',
                selectedBombero: null,
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
                return this.editedIndex === -1 ? 'Nueva Registro de Incendios' : 'Actualizar Registro de Incendios'
            }
        },

        watch: {
            dialog (val) {
            val || this.close()
            }
        },

        created () {
            this.listar();
            this.select();
        },
        methods:{
            listar(){
                let me=this;
                axios.get('api/RegistroIncendios/GetListAllasync').then(function(response){
                    //console.log(response);
                    me.RegistroIncendio=response.data.data;
                    console.log(data.data);
                }).catch(function(error){
                    console.log(error);
                });
            },
            editItem (item) {
                this.id=item.id;
                this.direccion=item.direccion;
                this.idBomberoCargo=item.idBomberoCargo;
                this.bomberoCargo=item.bomberoCargo;
                this.fechaCreacion=item.fechaCreacion;
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
                this.direccion="";
                this.bomberoCargo="";
                this.fechaCreacion="";
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
                    axios.put(`api/RegistroIncendios?id=${this.id}`,{
                  /*       'id':me.id, */ 
                        'direccion': me.direccion,
                        'idBomberoCargo': me.idBomberoCargo               
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
                    axios.post('api/RegistroIncendios',{
                        'direccion': me.direccion,
                        'idBomberoCargo': me.idBomberoCargo 
                    }).then(function(response){
                        me.close();
                        me.listar();
                        me.limpiar();                        
                    }).catch(function(error){
                        console.log(error);
                    });
                }
            },
            select(){
                let me=this;
                var BomberoArray=[];
                axios.get('api/Bomberos').then(function(response){
                    BomberoArray=response.data.data;
                    console.log(response.data.data);
                    BomberoArray.map(function(x){
                        me.clientes.push({text: x.nombre,value:x.id});
                    });
                }).catch(function(error){
                    console.log(error);
                });
            },
            validar(){
                this.valida=0;
                this.validaMensaje=[];

                if (this.direccion.length<3 || this.direccion.length>50){
                    this.validaMensaje.push("El nombre debe tener más de 3 caracteres y menos de 50 caracteres");
                }
                if (this.validaMensaje.length){
                    this.valida=1;
                }
                return this.valida;
            },
            activarDesactivarMostrar(accion,item){
                this.adModal=1;
                this.adNombre=item.direccion;
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

