function AddImage(slide, imagePath, scaleFactor, x, y) {
    return new Promise((resolve, reject) => {
        var img = new Image();
        img.onload = function() {
            var width = this.width/scaleFactor;
            var height = this.height/scaleFactor;
            slide.addImage({ 
                path:imagePath, 
                x:x, y:y, w:width, h:height }
            );
            resolve();
        }
        img.src = imagePath;
    });
}

function GeneratePresentation(config) {
    var pptx = new PptxGenJS();
    var images = [];

    var slide1 = pptx.addNewSlide();
    slide1.addText(
        'Azure Symbols', 
        { x:0, y:0, w:'100%', h:'100%', autoFit:true, align:'center', valign:'center', 
        color:'0072C6', fontSize:24, bold:true }
    );

    var slide2 = pptx.addNewSlide();
    slide2.addText(
        'Azure-PlantUML', 
        { x:0, y:0, w:'100%', h:0.5, autoFit:true, align:'center', valign:'center', 
        color:'0072C6', fontSize:20, bold:true }
    );
    slide2.addText(
        [
            { text:'This export of Azure Symbols is part of '},
            { text:'Azure-PlantUML', options:{  hyperlink:{ url:'https://github.com/RicardoNiepel/Azure-PlantUML' } }},
            { text:'.'},
        ],
        { x:2.5, y:0.5, w:5.1, fontSize:14, valign:'top'}
    );

    slide2.addText(
        [
            { text:'With Azure-PlantUML you can create high-level and technical diagrams:' }
        ],
        { x:1, y:1, w:3.3, fontSize:14, valign:'top'}
    );

    slide2.addText(
        [
            { text:'And you can combine this with ' },
            { text:'C4-PlantUML', options:{  hyperlink:{ url:'https://github.com/RicardoNiepel/C4-PlantUML' } } },
            { text:' for using the ' },
            { text:'C4 model for software architecture', options:{  hyperlink:{ url:'https://c4model.com/' } } },
            { text:':' },
        ],
        { x:5.75, y:1, w:3.3, fontSize:14, valign:'top'}
    );
    
    var imagePath1 = 'http://www.plantuml.com/plantuml/proxy?idx=0&src=https%3A%2F%2Fraw.githubusercontent.com%2FRicardoNiepel%2FAzure-PlantUML%2Fmaster%2Fsamples%2FTwo%2520Mode%2520Sample%2520-%2520Simplified.puml';
    images.push(new Promise((resolve, reject) => {
        var img = new Image();
        img.onload = function() {
            var width = this.width/250;
            var height = this.height/250;
            slide2.addImage({ 
                path:imagePath1, 
                x:0.65, y:1.8, w:3.4, h:1.75 }
            );
            resolve();
        }
        img.src = imagePath1;
    }));

    var imagePath2 = 'http://www.plantuml.com/plantuml/proxy?idx=0&src=https%3A%2F%2Fraw.githubusercontent.com%2FRicardoNiepel%2FAzure-PlantUML%2Fmaster%2Fsamples%2FTwo%2520Mode%2520Sample%2520-%2520Normal.puml';
    images.push(new Promise((resolve, reject) => {
        var img = new Image();
        img.onload = function() {
            var width = this.width/250;
            var height = this.height/250;
            slide2.addImage({ 
                path:imagePath2, 
                x:0.65, y:3.7, w:3.4, h:1.75 }
            );
            resolve();
        }
        img.src = imagePath2;
    }));

    var imagePath3 = 'http://www.plantuml.com/plantuml/proxy?idx=0&src=https%3A%2F%2Fraw.githubusercontent.com%2FRicardoNiepel%2FAzure-PlantUML%2Fmaster%2Fsamples%2FC4%2520usage%2520-%2520Highly%2520scalable%2520web%2520application.puml';
    images.push(AddImage(slide2, imagePath3, 310, 5.3, 1.9));

    var slide3 = pptx.addNewSlide();
    slide3.addText(
        [
            { text:'But sometimes you just want to use PowerPoint – that’s the reason the following slides exist.\n', options: { breakLine:true } },
            { text:'Please ensure to create always meaningful diagrams – I really recommend the '},
            { text:'software architecture diagram review checklist', options:{  hyperlink:{ url:'https://c4model.com/assets/software-architecture-diagram-review-checklist.pdf' } } },
            { text:'.\n', options: { breakLine:true } },
            { text:'All symbols are available as colorized and monochrome versions.', options: { breakLine:true } },
            { text:'My recommendation: use the monochrome symbols to avoid distracting your stakeholders.' },
        ],
        { x:2.2, y:1.3, w:5.5, h:3, fontSize:16, valign:'top'}
    );

    GenerateCategorySlides(config, pptx, images);

    Promise.all(images).then(values => {
        pptx.save('Azure_Symbols');
    });
}

function GenerateCategorySlides(config, pptx, images) {
    config.Categories.forEach(category => {
        GenerateCategorySlide(pptx, category, images, true);
        GenerateCategorySlide(pptx, category, images, false);
    });
}
function GenerateCategorySlide(pptx, category, images, useMonochrome) {
    var slide = pptx.addNewSlide();
    slide.addText(category.Name, {
        x: 0, y: 0, w: '100%', h: 0.5, autoFit: true, align: 'center', valign: 'center',
        color: '0072C6', fontSize: 20, bold: true
    });
    
    var topMargin = 0.5;
    var leftMargin = 0.5;

    var blockWidth = 1.5;
    var blockImageHeight = 1;
    var blockTextHeight = 0.5;
    var blockMarginLeft = 0.25;
    var blockMarginTop = 0.1;

    var row = 0;
    var column = 0;
    category.Services.forEach(service => {
        var imagePath = distFolder + category.Name + '/' + service.Target + (useMonochrome ? '(m)' : '') + '.svg';
        images.push(new Promise((resolve, reject) => {
            var innerRow = row;
            var innerColumn = column;
            var img = new Image();
            img.onload = function () {
                var width = this.width / 150;
                var x = leftMargin + innerColumn * (blockWidth + blockMarginLeft) + (blockWidth - width) / 2;
                var height = this.height / 150;
                var y = topMargin + innerRow * (blockImageHeight + blockTextHeight + blockMarginTop) + blockImageHeight - height;
                slide.addImage({
                    path: imagePath,
                    x: x, y: y, w: width, h: height
                });
                resolve();
            };
            img.src = imagePath;
        }));

        var name = service.Target.replace(/(?<!^)(([A-Z][a-z])|([A-Z][A-Z0-9][A-Z])|([A-Z][A-Z]?[a-z][A-Z]))/g, ' $1');
        var x = leftMargin + column * (blockWidth + blockMarginLeft);
        var y = topMargin + row * (blockImageHeight + blockTextHeight + blockMarginTop) + blockImageHeight;
        slide.addText(name, {
            x: x, y: y, w: blockWidth, h: blockTextHeight, autoFit: true, align: 'center', valign: 'top',
            color: '0072C6', fontSize: 12
        });

        if (column < 4) {
            column++;
        }
        else {
            row++;
            column = 0;
        }
    });
}

