(function (w, $) {
    if (typeof $ === "undefined") {
        return;
    }

    $.fn.floatTableFooter = function () {
        if (this.length <= 0) {
            return;
        }

        var table = this.first(),
            nav = $(".no-margin"),  // 導覽列的區塊
            //topH = 10,  // 離頂端的距離
            opacity = 0.3,  // 透明度
            color = "#EFF3F8", //"#ffffff",  // 若導覽列有底色, 請留下雙引號 "" 即可；若導覽列底色為透明, 建議自行設定導覽列底色, 例如 #ffffff 代表白色
            //initH = 600,//nav.offset().top, //不使用 改為連結底部
            //body = $(document),
            width = nav.width() + "px",
            //fixedCSS = { top: topH + "px", position: "fixed", "z-index": "10", opacity: opacity, width: width },
            fixedCSS = { bottom: "0px", position: "fixed", "z-index": "10", opacity: opacity, width: width },
            staticCSS = { position: "static", opacity: 1 };

        if (color) {
            staticCSS.backgroundColor = nav.css("backgroundColor");
            fixedCSS.backgroundColor = color
        }

        $(w).scroll(function () { 

            //基準高度(footer 應該出現於高度在多少的時候) 
            var fixedLine = table.offset().top + table.height(); //table起始高度到螢幕頂端的距離 + table本身的高度

            //與基準高度比較
            if (fixedLine >= $(document).scrollTop() + $(window).height() ) { //scroll滑動的距離加上螢幕的長度
                nav.css(fixedCSS);
                nav.on("mouseover", function () { nav.css("opacity", 1) });
                nav.on("mouseout", function () { nav.css("opacity", opacity) })
            }
            else {
                nav.css(staticCSS);
                nav.off("mouseover");
                nav.off("mouseout")
            }
        });

        $(w).resize(function () {
            fixedCSS.width = table.width() + "px";
            nav.width(table.width());
        });
    };
})(window, jQuery);
