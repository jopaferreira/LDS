APIFirst - Aplicação para Gerar e Manipular ficheiros PDF

APIFirst é uma aplicação desenvolvida em C# que segue o padrão de arquitetura Model-View-Controller (MVC) para gerar e 
manipular documentos PDF. Esta aplicação permite gerar documentos PDF a partir de um título e conteúdo inseridos pelo utilizador e
permite também concatenar vários ficheiros PDF num único documento PDF.

Estrutura de ficheiros:
CONTROLLERS
	PdfController.cs: Responsável por controlar a criação de documentos PDF. Contém métodos para criar um novo documento PDF com
	título e conteúdo especificados.

	MainController.cs: Controla a interação entre a interface do utilizador e as funcionalidades da aplicação. Gere eventos 
	relacionados com a criação e manipulação de PDFs.

MODELS
	PdfDocumentModel.cs: Representa o modelo de dados para um documento PDF. Possui propriedades para título e conteúdo do documento.

VIEWS
	MainForm.cs: Apresenta a interface principal da aplicação, permitindo ao utilizador gerar documentos PDF e concatenar ficheiros PDF.

	OptionForm.cs: Oferece opções para gerar um novo documento PDF ou concatenar ficheiros PDF existentes.

	InputBox.cs: Classe auxiliar para exibir caixas de diálogo de entrada de texto.

	SplashScreen.cs: Apresenta um ecrã de introdução enquanto a aplicação carrega.

OUTROS
	CustomFontResolver.cs: Permite a utilizaçaõ de fontes personalizados na criação de PDFs.

	Program.cs: Ponto de entrada da aplicação, configura a utilização de fontes e inicia a interface do utilizador.


Contribuição
Contribuições para melhorias na aplicação são bem-vindas. Para contribuir, siga estas etapas:

Faça um fork do repositório.
Crie uma branch para a sua modificação -"feature"" (git checkout -b feature/NovaFeature).
Faça commit das suas alterações (git commit -am 'Adiciona NovaFeature').
Faça push para a branch (git push origin feature/NovaFeature).
Crie um novo Pull Request.
