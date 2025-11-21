$(document).ready(function () {

    // Abre modal de beneficiários
    $("#btnModalBenef").click(function () {
        $("#modalBeneficiario").modal("show");
        carregarBeneficiarios();
    });

    // Adiciona beneficiário na tabela do modal
    $("#btnAddBenef").click(function () {
        let cpf = $("#cpfBenef").val().trim();
        let nome = $("#nomeBenef").val().trim();

        if (!cpf || !nome) {
            alert("Preencha CPF e Nome.");
            return;
        }

        if (!validarCPF(cpf)) {
            alert("CPF inválido.");
            return;
        }

        $("#gridBenef tbody").append(`
            <tr>
                <td>${cpf}</td>
                <td>${nome}</td>
                <td><button class="btn btn-danger btn-sm btnDelBenef">Excluir</button></td>
            </tr>
        `);

        $("#cpfBenef").val("");
        $("#nomeBenef").val("");
    });

    // Remove beneficiário
    $(document).on("click", ".btnDelBenef", function () {
        $(this).closest("tr").remove();
    });

});

// Função que carrega beneficiários do servidor
function carregarBeneficiarios() {
    let idCliente = $("#Id").val();
    if (!idCliente) return;

    $.get(`/Beneficiario/Listar/${idCliente}`, function (data) {
        $("#gridBenef tbody").empty();

        if (data.Resultado) {
            data.Records.forEach(b => {
                $("#gridBenef tbody").append(`
                    <tr>
                        <td>${b.CPF}</td>
                        <td>${b.Nome}</td>
                        <td><button class="btn btn-danger btn-sm btnDelBenef">Excluir</button></td>
                    </tr>
                `);
            });
        }
    });
}

// Função para validar CPF
function validarCPF(cpf) {
    cpf = cpf.replace(/\D/g, '');
    if (cpf.length !== 11) return false;
    if (/^(\d)\1{10}$/.test(cpf)) return false;

    let soma = 0, resto;
    for (let i = 1; i <= 9; i++)
        soma += parseInt(cpf.substring(i - 1, i)) * (11 - i);

    resto = (soma * 10) % 11;
    if (resto === 10 || resto === 11) resto = 0;
    if (resto !== parseInt(cpf.substring(9, 10))) return false;

    soma = 0;
    for (let i = 1; i <= 10; i++)
        soma += parseInt(cpf.substring(i - 1, i)) * (12 - i);

    resto = (soma * 10) % 11;
    if (resto === 10 || resto === 11) resto = 0;
    return resto === parseInt(cpf.substring(10, 11));
}
