$(document).ready(function () {

    $('#formCadastro').submit(function (e) {

        e.preventDefault();

        
        let beneficiarios = [];

        $("#gridBenef tbody tr").each(function () {
            beneficiarios.push({
                CPF: $(this).find("td:eq(0)").text(),
                Nome: $(this).find("td:eq(1)").text()
            });
        });

       
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
                "Telefone": $(this).find("#Telefone").val(),

                
                "Beneficiarios": beneficiarios
            },

            error: function (r) {
                if (r.status == 400)
                    ModalDialog("Ocorreu um erro", r.responseJSON);
                else if (r.status == 500)
                    ModalDialog("Ocorreu um erro", "Ocorreu un erro interno no servidor.");
            },

            success: function (r) {
                ModalDialog("Sucesso!", r)
                $("#formCadastro")[0].reset();

                $("#gridBenef tbody").empty();
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
