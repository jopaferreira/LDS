O Padrão MVC
O padrão MVC separa a aplicação em três componentes principais:

Modelo (Model): Gerencia os dados e a lógica de negócios da aplicação.
Visão (View): Responsável pela apresentação dos dados e interação com o usuário.
Controlador (Controller): Intermediário que lida com a lógica da aplicação e coordena as interações entre o Modelo e a Visão.


Estrutura do Código

1. Modelo (Model)

O modelo define a estrutura dos dados que a aplicação manipula. Neste caso, o modelo é a classe Relatorio.
Relatorio: Esta classe representa o relatório, com propriedades Titulo e Dados. É responsável por armazenar os dados do relatório.

2. Visão (View)

A visão é responsável pela apresentação dos dados ao usuário e pela interação com ele.
Neste caso, a visão é dividida entre a interface gráfica (Form1) e a classe PdfView que gera o PDF.
Form1: Esta classe representa a interface gráfica da aplicação.
Aqui, o usuário pode inserir o título e os dados do relatório e clicar em um botão para gerar o PDF.
PdfView: Esta classe é responsável por gerar o PDF. Recebe um objeto Relatorio e cria um documento PDF usando a biblioteca PDFSharp.

3. Controlador (Controller)

O controlador lida com a lógica da aplicação e coordena as interações entre o modelo e a visão.
RelatorioController: Esta classe atua como o controlador, coordenando a criação do modelo Relatorio e a geração do PDF através da PdfView.

Resumo do Fluxo MVC
O usuário interage com a interface gráfica (Form1): Insere o título e os dados, e clica no botão "Gerar Relatório".
O controlador (RelatorioController) é chamado: O método btnGerarRelatorio_Click chama o método GerarRelatorio do controlador, 
passando o título e os dados.
O controlador cria o modelo (Relatorio): O controlador cria uma instância de Relatorio com os dados fornecidos.
O controlador solicita a visão (PdfView) para gerar o PDF: A PdfView usa o modelo Relatorio para gerar o documento PDF 
e salvá-lo no caminho especificado.
O usuário é notificado sobre a conclusão: A interface gráfica exibe uma mensagem indicando que o relatório foi gerado com sucesso.
Dessa forma, o padrão MVC é aplicado para separar as responsabilidades e tornar o código mais modular e fácil de manter.

VERIFICAR!