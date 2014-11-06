/// <reference path="typings/require/require.d.ts" />
/// <reference path="typings/jquery/jquery.d.ts" />
/// <reference path="typings/knockout/knockout.d.ts" />
/// <reference path="typings/knockout.mapping/knockout.mapping.d.ts" />
/// <reference path="typings/toastr/toastr.d.ts" />
define(["require", "exports", 'knockout', 'jquery'], function(require, exports, __ko__, __$__) {
    var ko = __ko__;
    var $ = __$__;

    var exportador = (function () {
        function exportador(id, nom, telef, modo) {
            var _this = this;
            this.setDisplayMode = function () {
                _this.mode("display");
            };
            this.exportadorId = ko.observable(id);
            this.nombre = ko.observable(nom);
            this.telefono = ko.observable(telef);
            this.mode = ko.observable(modo);

            this.nombre_prev = nom;
            this.telefono_prev = telef;
        }
        exportador.fromJson = function (data) {
            return new exportador(data.ExportadorId, data.Nombre, data.Telefono, "display");
        };

        exportador.prototype.setEditMode = function () {
            this.mode("edit");
        };

        exportador.prototype.cancelar = function () {
            this.nombre(this.nombre_prev);
            this.telefono(this.telefono_prev);
            this.setDisplayMode();
        };

        exportador.prototype.serialize = function () {
            var obj = this;

            //var serialString : string = '{ "ExportadorId":' + this.exportadorId() + ', "nombre": "' + this.nombre() + '", "telefono": "' + this.telefono() + '" }';
            var serialString = ko.toJSON(this);

            return serialString;
        };

        exportador.prototype.grabar = function (exp) {
            if (exp.nombre() != exp.nombre_prev || exp.telefono() != exp.telefono_prev) {
                var elNuevoId;
                var jsonString = this.serialize();

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
                    });
                } else {
                    httpAction = "POST";

                    $.ajax({
                        type: httpAction,
                        contentType: "application/json",
                        url: "/breeze/exportadores/Get",
                        data: jsonString
                    }).done(function (resp) {
                        var x = exportador.fromJson(resp);
                        self.exportadorId(x.exportadorId());
                    });
                }

                this.nombre_prev = exp.nombre();
                this.telefono_prev = exp.telefono();
                this.setDisplayMode();
            } else {
                toastr.error('Los datos no han cambiado.');
            }
        };
        return exportador;
    })();

    var exportadorViewModel = (function () {
        function exportadorViewModel() {
            this.exportadores = ko.observableArray([]);

            this.pageSize = ko.observable(2);
            this.currentPageIndex = ko.observable(0);

            this.currentPage = ko.computed({
                owner: this,
                read: function () {
                    var tmp1, tmp2, tmp3, startIndex, endIndex;
                    tmp1 = this.pageSize;
                    tmp2 = this.currentPageIndex;
                    tmp3 = this.pageSize;
                    startIndex = tmp1 * tmp2;
                    endIndex = startIndex + tmp3;
                    return this.exportadores.slice(startIndex, endIndex);
                }
            });
        }
        exportadorViewModel.prototype.nextPage = function () {
            if (((this.currentPageIndex() + 1) * this.pageSize()) < this.exportadores().length) {
                this.currentPageIndex(this.currentPageIndex() + 1);
            } else {
                this.currentPageIndex(0);
            }
        };

        exportadorViewModel.prototype.previousPage = function () {
            if (this.currentPageIndex() > 0) {
                this.currentPageIndex(this.currentPageIndex() - 1);
            } else {
                this.currentPageIndex((Math.ceil(this.exportadores().length / this.pageSize())) - 1);
            }
        };

        exportadorViewModel.prototype.crearNuevoExportador = function () {
            var exp = new exportador(-1, "", "", "edit");
            this.exportadores.push(exp);
        };

        exportadorViewModel.prototype.borrarExportador = function (exp) {
            $.ajax({
                type: "DELETE",
                contentType: "application/json",
                url: "/breeze/exportadores/Delete" + exp.exportadorId()
            });

            this.exportadores.remove(exp);
        };
        return exportadorViewModel;
    })();

    require(['jquery'], function ($) {
        $(document).ready(function () {
            var loadingDiv = document.getElementById("cargandoListaExportadores");

            var viewModel = new exportadorViewModel();

            $.getJSON("/breeze/exportadores/Get", function (data) {
                ko.utils.arrayForEach(data, function (item) {
                    var x = exportador.fromJson(item);
                    viewModel.exportadores.push(x);
                });
            });

            console.log(viewModel.currentPage());

            loadingDiv.hidden = true;

            ko.applyBindings(viewModel);
        });
    });
});
//# sourceMappingURL=catalogoexportadores.js.map
