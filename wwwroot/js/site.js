// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


$(document).ready(function () {

    $('#tableCliente').DataTable(
        {
            "lengthMenu": [[5, 10, 25, 50, -1], [5, 10, 25, 50, "All"]],
            "language": {
                search: '<i class="fas fa-search" aria-hidden="true"></i>',
                searchPlaceholder: 'Buscar por ...',
                emptyTable: "Nenhum registro encontrado",
                info: "Mostrando de _START_ até _END_ de _TOTAL_ registros",
                infoEmpty: "Mostrando 0 até 0 de 0 registros",
                infoFiltered: "(Filtrados de _MAX_ registros)",
                infoThousands: ".",
                loadingRecords: "Carregando...",
                processing: "Processando...",
                zeroRecords: "Nenhum registro encontrado",
                paginate: {
                    "next": "Proxima",
                    "previous": "Anterior",
                    "first": "Primeiro",
                    "last": "Último"
                },
                buttons: {
                    "copySuccess": {
                        "1": "Uma linha copiada com sucesso",
                        "_": "%d linhas copiadas com sucesso"
                    },
                    "collection": "Coleção  <span class=\"ui-button-icon-primary ui-icon ui-icon-triangle-1-s\"><\/span>",
                    "colvis": "Visibilidade da Coluna",
                    "colvisRestore": "Restaurar Visibilidade",
                    "copy": "Copiar",
                    "copyKeys": "Pressione ctrl ou u2318 + C para copiar os dados da tabela para a área de transferência do sistema. Para cancelar, clique nesta mensagem ou pressione Esc..",
                    "copyTitle": "Copiar para a Área de Transferência",
                    "csv": "CSV",
                    "excel": "Excel",
                    "pageLength": {
                        "-1": "Mostrar todos os registros",
                        "1": "Mostrar 1 registro",
                        "_": "Mostrar %d registros"
                    },
                    "pdf": "PDF",
                    "print": "Imprimir"
                },
                select: {
                    "rows": {
                        "_": "Selecionado %d linhas",
                        "0": "Nenhuma linha selecionada",
                        "1": "Selecionado 1 linha"
                    },
                    "1": "%d linha selecionada",
                    "_": "%d linhas selecionadas",
                    "cells": {
                        "1": "1 célula selecionada",
                        "_": "%d células selecionadas"
                    },
                    "columns": {
                        "1": "1 coluna selecionada",
                        "_": "%d colunas selecionadas"
                    }
                },
                lengthMenu: "Exibir _MENU_ resultados por página",
            }
        }
    );


    var url_atual = window.location.href;
    if (url_atual.match("Clientes")) {

        if (document.getElementById('radio1').checked) {
            toogle('f');
        } else if (document.getElementById('radio2').checked) {
            toogle('j');
        }

        //Mask Cpf e Cnpj
        var options = {
            onKeyPress: function (cpf, ev, el, op) {
                var masks = ['000.000.000-000', '00.000.000/0000-00'],
                    mask = (cpf.length > 14) ? masks[1] : masks[0];
                el.mask(mask, op);
            }
        }
        $('.cnpjCpf').mask('000.000.000-000', options);
    }

    if (url_atual.match("Clientes/Edit")) {
        $('#Cep').mask('00000-000');
    }

    function limpa_formulário_cep() {
        // Limpa valores do formulário de cep.
        $("#Cep").val("");
        $("#Logradouro").val("");
        $("#Bairro").val("");
        $("#Cidade").val("");
        $("#Uf").val("");
        //$("#ibge").val("");
    }

    //Quando o campo cep perde o foco.
    $("#Cep").blur(function () {

        //Nova variável "cep" somente com dígitos.
        var cep = $(this).val().replace(/\D/g, '');

        console.log(cep);

        //Verifica se campo cep possui valor informado.
        if (cep != "") {

            //Expressão regular para validar o CEP.
            var validacep = /^[0-9]{8}$/;

            //Valida o formato do CEP.
            if (validacep.test(cep)) {

                document.getElementById('Cep').value = cep.substring(0, 5)
                    + "-"
                    + cep.substring(5);

                //Preenche os campos com "..." enquanto consulta webservice.
                $("#Logradouro").val("...");
                $("#Bairro").val("...");
                $("#Cidade").val("...");
                $("#Uf").val("...");
                //$("#ibge").val("...");

                //Consulta o webservice viacep.com.br/
                $.getJSON("https://viacep.com.br/ws/" + cep + "/json/?callback=?", function (dados) {

                    if (!("erro" in dados)) {
                        //Atualiza os campos com os valores da consulta.
                        $("#Logradouro").val(dados.logradouro);
                        $("#Bairro").val(dados.bairro);
                        $("#Cidade").val(dados.localidade);
                        $("#Uf").val(dados.uf);
                        //$("#ibge").val(dados.ibge);
                    } //end if.
                    else {
                        //CEP pesquisado não foi encontrado.
                        limpa_formulário_cep();
                        alert("CEP não encontrado.");
                    }
                });
            } //end if.
            else {
                //cep é inválido.
                limpa_formulário_cep();
                alert("Formato de CEP inválido.");
            }
        } //end if.
        else {
            //cep sem valor, limpa formulário.
            limpa_formulário_cep();
        }
    });
});


function toogle(e) {

    var sobrenome = document.getElementById("DivSobrenome");
    var dataNascimento = document.getElementById("DivDataNascimento");

    var nomeFantasia = document.getElementById("DivNomeFantasia");

    if (e == 'j') {

        sobrenome.style.display = "none";
        dataNascimento.style.display = "none";

        nomeFantasia.style.display = "block";

        document.getElementById('labelNome_RazaoSocial').innerHTML = 'Razão Social';
        document.getElementById('labelCnpj_Cpf').innerHTML = 'CNPJ';

        $('#Cnpj_Cpf').mask('00.000.000/0000-00', { reverse: true });

    } else if (e == 'f') {

        sobrenome.style.display = "block";
        dataNascimento.style.display = "block";

        nomeFantasia.style.display = "none";

        document.getElementById('labelNome_RazaoSocial').innerHTML = 'Nome';
        document.getElementById('labelCnpj_Cpf').innerHTML = 'CPF';

        $('#Cnpj_Cpf').mask('000.000.000-00', { reverse: true });
        
    }
}






