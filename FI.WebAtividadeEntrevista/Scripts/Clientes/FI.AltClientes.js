$.validator.addMethod("cpf", function (value, element) {



    value = value.replace(/[^\d]+/g, '');



    if (value.length !== 11 || /^(\d)/1{ 10 } $ /.test(value))

{

    return false;



}



var soma = 0, resto;



for (var i = 1; i <= 9; i++) {

    soma = soma + parseInt(value.substring(i - 1, i)) * (11 - i);

}



resto = (soma * 10) % 11;



if ((resto === 10) || (resto === 11)) resto = 0;



if (resto !== parseInt(value.substring(9, 10)))



    return false;



soma = 0;



for (var i = 1; i <= 10; i++) {

    soma = soma + parseInt(value.substring(i - 1, i)) * (12 - i);

}



resto = (soma * 10) % 11;



if ((resto === 10) || (resto === 11)) resto = 0;



if (resto !== parseInt(value.substring(10, 11)))

    return false;



return true;



        }, "Informe um CPF válido.");



$(document).ready(function () {



    if (obj) {



        $('#formCadastro #Nome').val(obj.Nome);

        $('#formCadastro #CEP').val(obj.CEP);

        $('#formCadastro #Email').val(obj.Email);

        $('#formCadastro #CPF').val(obj.CPF);

        $('#formCadastro #Sobrenome').val(obj.Sobrenome);

        $('#formCadastro #Nacionalidade').val(obj.Nacionalidade);

        $('#formCadastro #Estado').val(obj.Estado);

        $('#formCadastro #Cidade').val(obj.Cidade);

        $('#formCadastro #Logradouro').val(obj.Logradouro);

        $('#formCadastro #Telefone').val(obj.Telefone);

    }



    $('#CPF').mask('000.000.000-00');

    $('#formCadastro').validate({



        rules: {

            CPF: required: true,

            cpf: true

        }

    },



        messages: {

        CPF: {

            required: "O campo CPF é obrigatório.",

            cpf: "O CPF informado é inválido."

        }

    });



    $('#formCadastro').submit(function (e) {

        e.preventDefault();



        if (!$("#formCadastro").valid()) {

            return false;

        }



        $.ajax({

            url: urlPost,

            method: "POST",

            data: {

                "NOME": $(this).find("#Nome").val(),

                "CEP": $(this).find("#CEP").val(),

                "Email": $(this).find("#Email").val(),

                "CPF": $(this).find("#CPF").val(),

                "Sobrenome": $(this).find("#Sobrenome").val(),

                "Nacionalidade": $(this).find("#Nacionalidade").val(),

                "Estado": $(this).find("#Estado").val(),

                "Cidade": $(this).find("#Cidade").val(),

                "Logradouro": $(this).find("#Logradouro").val(),

                "Telefone": $(this).find("#Telefone").val()

            },



            error:

                function (r) {

                    if (r.status == 400)

                        ModalDialog("Ocorreu um erro", r.responseJSON);

                    else if (r.status == 500)

                        ModalDialog("Ocorreu um erro", "Ocorreu um erro interno no servidor.");

                },

            success:

                function (r) {

                    ModalDialog("Sucesso!", r)

                    $("#formCadastro")[0].reset();

                    window.location.href = urlRetorno;

                }

        });

    })

})



function ModalDialog(titulo, texto) {
    var random = Math.random().toString().replace('.', '');

    var modal =
        '<div id="' + random + '" class="modal fade">' +
        '  <div class="modal-dialog">' +
        '    <div class="modal-content">' +
        '      <div class="modal-header">' +
        '        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>' +
        '        <h4 class="modal-title">' + titulo + '</h4>' +
        '      </div>' +
        '      <div class="modal-body">' +
        '        <p>' + texto + '</p>' +
        '      </div>' +
        '      <div class="modal-footer">' +
        '        <button type="button" class="btn btn-default" data-dismiss="modal">Fechar</button>' +
        '      </div>' +
        '    </div>' +
        '  </div>' +
        '</div>';

    $('body').append(modal);
    $('#' + random).modal('show');
}