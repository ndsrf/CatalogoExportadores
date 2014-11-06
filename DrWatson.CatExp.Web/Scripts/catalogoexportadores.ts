/// <reference path="typings/require/require.d.ts" />
/// <reference path="typings/jquery/jquery.d.ts" />
/// <reference path="typings/knockout/knockout.d.ts" />
/// <reference path="typings/knockout.mapping/knockout.mapping.d.ts" />
/// <reference path="typings/toastr/toastr.d.ts" />

import ko = require('knockout')
import $ = require('jquery');


class exportador {
    public exportadorId: KnockoutObservable<number>;
    public nombre: KnockoutObservable<string>;
    public telefono: KnockoutObservable<string>;
    public mode: KnockoutObservable<string>;

    public nombre_prev: string;
    public telefono_prev: string;


    static fromJson(data: any) {
        return new exportador(data.ExportadorId, data.Nombre, data.Telefono, "display");
    }

    constructor(id: number, nom: string, telef: string, modo: string) {
        this.exportadorId = ko.observable(id);
        this.nombre = ko.observable(nom);
        this.telefono = ko.observable(telef);
        this.mode = ko.observable(modo);

        this.nombre_prev = nom;
        this.telefono_prev = telef;

    }

    setEditMode() {
        this.mode("edit");
    }
    
    setDisplayMode = () => {
        this.mode("display");
    }

    cancelar() {
        this.nombre(this.nombre_prev);
        this.telefono(this.telefono_prev);
        this.setDisplayMode();
    }

    serialize() {
        var obj = this;
        //var serialString : string = '{ "ExportadorId":' + this.exportadorId() + ', "nombre": "' + this.nombre() + '", "telefono": "' + this.telefono() + '" }';

        var serialString: string = ko.toJSON(this);

        return serialString;
    }

    grabar(exp: exportador) {

        if (exp.nombre() != exp.nombre_prev || exp.telefono() != exp.telefono_prev) {

            var elNuevoId: number;
            var jsonString: string = this.serialize();
            //console.log(exp.serialize());
            //console.log(jsonString);
            var self = this;
            var httpAction;

            if (this.exportadorId() > 0) {
                httpAction = "PUT";

                $.ajax({
                    type: httpAction,
                    contentType: "application/json",
                    url: "/breeze/exportadores/Put" + this.exportadorId(),
                    data: jsonString
                })
            }
            else {
                httpAction = "POST";

                $.ajax({
                    type: httpAction,
                    contentType: "application/json",
                    url: "/breeze/exportadores/Get",
                    data: jsonString
                }).done(function (resp: any) {
                        var x = exportador.fromJson(resp);
                        self.exportadorId(x.exportadorId());

                    });
            }

            this.nombre_prev = exp.nombre();
            this.telefono_prev = exp.telefono();
            this.setDisplayMode();

        } else {
            toastr.error('Los datos no han cambiado.')
        }

    } 
}

class exportadorViewModel {
    public exportadores: KnockoutObservableArray<exportador>;

    // Paginación
    public currentPageIndex: KnockoutObservable<number>;
    public pageSize: KnockoutObservable<number>;
    currentPage: KnockoutComputed<KnockoutObservableArray<exportador>>;

    nextPage() {
        if (((this.currentPageIndex() + 1) * this.pageSize()) < this.exportadores().length) {
            this.currentPageIndex(this.currentPageIndex() + 1);
        }
        else {
            this.currentPageIndex(0);
        }
    }

    previousPage() {
        if (this.currentPageIndex() > 0) {
            this.currentPageIndex(this.currentPageIndex() - 1);
        }
        else {
            this.currentPageIndex((Math.ceil(this.exportadores().length / this.pageSize())) - 1);
        }
    }


    crearNuevoExportador()
    {
        var exp:exportador = new exportador(-1, "", "", "edit");
        this.exportadores.push(exp);
    }

    constructor() {
        this.exportadores = ko.observableArray([]);

        this.pageSize = ko.observable(2);
        this.currentPageIndex= ko.observable(0);

        this.currentPage = ko.computed<any>({
            owner: this,
            read: function () {
                var tmp1, tmp2, tmp3, startIndex, endIndex: number;
                tmp1 = this.pageSize;
                tmp2 = this.currentPageIndex;
                tmp3 = this.pageSize;
                startIndex = tmp1 * tmp2;
                endIndex = startIndex + tmp3;
                return this.exportadores.slice(startIndex, endIndex);
            }
        });

    }

    public borrarExportador(exp: exportador) {
        $.ajax({
            type: "DELETE",
            contentType: "application/json",
            url: "/breeze/exportadores/Delete" + exp.exportadorId()
        })


        this.exportadores.remove(exp);
    }
}

require(['jquery'], function ($) {
    $(document).ready(function () {
        var loadingDiv = document.getElementById("cargandoListaExportadores");

        var viewModel = new exportadorViewModel();

        $.getJSON("/breeze/exportadores/Get",function (data: any) {
                ko.utils.arrayForEach(data, item => {
                    var x = exportador.fromJson(item);
                    viewModel.exportadores.push(x);
                });
            });

        console.log(viewModel.currentPage());


        loadingDiv.hidden = true;

        ko.applyBindings(viewModel);
    });


});
