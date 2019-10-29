(function ($) {
    'use strict';

    if ($ === null || typeof $ === 'undefined') {
        console.error('jQuery required not found.');
        return;
    }


    window.PTC.Select2 = window.PTC.Select2 || {};

    window.PTC.Select2.Kind = function (largeCategory,
                                        middleCategory,
                                        smallCategory) {


        window.PTC.HierarchyAjaxSelect2([
          /*大分類*/
          {
              elementName: largeCategory.name,
              url: largeCategory.url,
              placeholder: "請輸入大分類",
              getTransData: function (isReverse) {

                  var object = {};
                  var subData = $(middleCategory.name).select2('data')[0];
                  if (isReverse) {

                      object.CompCd = (subData) ? subData.addition.CompCd : null;
                      object.KindCd = (subData) ? subData.addition.ParentId : null;

                  } else {

                  }

                  object.TypeId = window.PTC.Enum.AssetKindType.LargeCategory;
                  object.isReverse = isReverse;

                  return object;

              }
          },
          /*中分類*/
          {
              elementName: middleCategory.name,
              url: middleCategory.url,
              placeholder: "請輸入中分類",
              getTransData: function (isReverse) {

                  var object = {};
                  var subData = $(smallCategory.name).select2('data')[0];
                  var prevData = $(largeCategory.name).select2('data')[0];

                  if (isReverse) {

                      object.CompCd = (subData) ? subData.addition.CompCd : null;
                      object.KindCd = (subData) ? subData.addition.ParentId : null;

                  } else {

                      object.CompCd = prevData.addition.CompCd;
                      object.ParentId = prevData.addition.KindCd;

                  }
                  object.TypeId = window.PTC.Enum.AssetKindType.MiddleCategory;
                  object.isReverse = isReverse;

                  return object;
              }

          },
          /*小分類*/
          {
              elementName: smallCategory.name,
              url: smallCategory.url,
              placeholder: "請輸入小分類",
              getTransData: function (isReverse) {

                  var object = {};
                  var prevData = $(middleCategory.name).select2('data')[0];

                  if (isReverse) {


                  } else {

                      object.CompCd = prevData.addition.CompCd;
                      object.ParentId = prevData.addition.KindCd;

                  }

                  object.TypeId = window.PTC.Enum.AssetKindType.SmallCategory;
                  object.isReverse = isReverse;

                  return object;
              }
          },
        ]);

    }


    window.PTC.Select2.Assets = function (largeCategory,
                                          middleCategory,
                                          smallCategory,
                                          assets) {

        window.PTC.HierarchyAjaxSelect2([
        /*大分類*/
        {
            elementName: largeCategory.name,
            url: largeCategory.url,
            placeholder: "請輸入大分類",
            getTransData: function (isReverse) {

                var object = {};
                var subData = $(middleCategory.name).select2('data')[0];
                if (isReverse) {

                    object.CompCd = (subData) ? subData.addition.CompCd : null;
                    object.KindCd = (subData) ? subData.addition.ParentId : null;;

                } else {
                }
                object.TypeId = window.PTC.Enum.AssetKindType.LargeCategory;
                object.isReverse = isReverse;
                return object;

            }
        },
        /*中分類*/
        {
            elementName: middleCategory.name,
            url: middleCategory.url,
            placeholder: "請輸入中分類",
            getTransData: function (isReverse) {

                var object = {};
                var subDataOfKind3 = $(smallCategory.name).select2('data')[0];
                var subDataOfAssets = $(assets.name).select2('data')[0];
                var prevData = $(largeCategory.name).select2('data')[0];


                if (isReverse) {

                    if (!subDataOfKind3 || !subDataOfKind3.addition) {

                        object.CompCd = subDataOfAssets ? subDataOfAssets.addition.CompCd : null;
                        object.KindCd = subDataOfAssets ? subDataOfAssets.addition.AstKind2 : null;

                    } else {

                        object.CompCd = subDataOfKind3 ? subDataOfKind3.addition.CompCd : null;
                        object.KindCd = subDataOfKind3 ? subDataOfKind3.addition.ParentId : null;
                    }

                } else {

                    object.CompCd = prevData.addition.CompCd;
                    object.ParentId = prevData.addition.KindCd;

                }

                object.TypeId = window.PTC.Enum.AssetKindType.MiddleCategory;
                object.isReverse = isReverse;

                return object;
            }

        },
        /*小分類*/
        {
            elementName: smallCategory.name,
            url: smallCategory.url,
            placeholder: "請輸入小分類",
            getTransData: function (isReverse) {

                var object = {};
                var prevData = $(middleCategory.name).select2('data')[0];
                var subData = $(assets.name).select2('data')[0];


                if (isReverse) {

                    object.CompCd = subData ? subData.addition.CompCd : null;
                    object.KindCd = subData ? subData.addition.AstKind3 : null;

                } else {

                    object.CompCd = prevData.addition.CompCd;
                    object.ParentId = prevData.addition.KindCd;
                }

                object.TypeId = window.PTC.Enum.AssetKindType.SmallCategory;
                object.isReverse = isReverse;

                return object;
            }
        },
        /*設備*/
        {
            elementName: assets.name,
            url: assets.url,
            placeholder: "請輸入設備",
            getTransData: function (isReverse) {

                var object = {};

                var kind1 = $(largeCategory.name).select2('data')[0];
                var kind2 = $(middleCategory.name).select2('data')[0];
                var kind3 = $(smallCategory.name).select2('data')[0];

                if (isReverse) {



                } else {

                    object.CompCd = kind1.addition ? kind1.addition.CompCd : null;
                    object.AstKind1 = kind1.addition ? kind1.addition.KindCd : null;
                    object.AstKind2 = kind2.addition ? kind2.addition.KindCd : null;
                    object.AstKind3 = kind3.addition ? kind3.addition.KindCd : null;
                }


                object.isReverse = isReverse;

                return object;
            }
        }
        ]);

    }

    window.PTC.Select2.AssetsWithComp = function (company,
                                                  largeCategory,
                                                  middleCategory,
                                                  smallCategory,
                                                  assets) {
      window.PTC.HierarchyAjaxSelect2([
       /*公司*/
       {
           elementName: company.name,
           url: company.url,
           placeholder: "請輸入公司",
           getTransData: function (isReverse) {

               var object = {};
               var subData = $(largeCategory.name).select2('data')[0];
               if (isReverse) {

                   object.CompCd = (subData) ? subData.addition.CompCd : null;

               } else {

               }

               object.isReverse = isReverse;
               return object;

           }
       },
       /*大分類*/
       {
          elementName: largeCategory.name,
          url: largeCategory.url,
          placeholder: "請輸入大分類",
          getTransData: function (isReverse) {

              var object = {};
              var subData = $(middleCategory.name).select2('data')[0];
              var prevData = $(company.name).select2('data')[0];
              if (isReverse) {

                  object.CompCd = (subData) ? subData.addition.CompCd : null;
                  object.KindCd = (subData) ? subData.addition.ParentId : null;;

              } else {

                  object.CompCd = (prevData) ? prevData.addition.CompCd : null;
              }
              object.TypeId = window.PTC.Enum.AssetKindType.LargeCategory;
              object.isReverse = isReverse;
              return object;

          }
      },
       /*中分類*/
       {
          elementName: middleCategory.name,
          url: middleCategory.url,
          placeholder: "請輸入中分類",
          getTransData: function (isReverse) {

              var object = {};
              var subDataOfKind3 = $(smallCategory.name).select2('data')[0];
              var subDataOfAssets = $(assets.name).select2('data')[0];
              var prevData = $(largeCategory.name).select2('data')[0];


              if (isReverse) {

                  if (!subDataOfKind3 || !subDataOfKind3.addition) {

                      object.CompCd = subDataOfAssets ? subDataOfAssets.addition.CompCd : null;
                      object.KindCd = subDataOfAssets ? subDataOfAssets.addition.AstKind2 : null;

                  } else {

                      object.CompCd = subDataOfKind3 ? subDataOfKind3.addition.CompCd : null;
                      object.KindCd = subDataOfKind3 ? subDataOfKind3.addition.ParentId : null;
                  }

              } else {

                  object.CompCd = prevData.addition.CompCd;
                  object.ParentId = prevData.addition.KindCd;

              }

              object.TypeId = window.PTC.Enum.AssetKindType.MiddleCategory;
              object.isReverse = isReverse;

              return object;
          }

      },
       /*小分類*/
       {
          elementName: smallCategory.name,
          url: smallCategory.url,
          placeholder: "請輸入小分類",
          getTransData: function (isReverse) {

              var object = {};
              var prevData = $(middleCategory.name).select2('data')[0];
              var subData = $(assets.name).select2('data')[0];


              if (isReverse) {

                  object.CompCd = subData ? subData.addition.CompCd : null;
                  object.KindCd = subData ? subData.addition.AstKind3 : null;

              } else {

                  object.CompCd = prevData.addition.CompCd;
                  object.ParentId = prevData.addition.KindCd;
              }

              object.TypeId = window.PTC.Enum.AssetKindType.SmallCategory;
              object.isReverse = isReverse;

              return object;
          }
      },
       /*設備*/
       {
          elementName: assets.name,
          url: assets.url,
          placeholder: "請輸入設備",
          getTransData: function (isReverse) {

              var object = {};

              var comp  = $(company.name).select2('data')[0];
              var kind1 = $(largeCategory.name).select2('data')[0];
              var kind2 = $(middleCategory.name).select2('data')[0];
              var kind3 = $(smallCategory.name).select2('data')[0];

              if (isReverse) {



              } else {

                  object.CompCd   = comp.addition ? comp.addition.CompCd : null;
                  object.AstKind1 = kind1.addition ? kind1.addition.KindCd : null;
                  object.AstKind2 = kind2.addition ? kind2.addition.KindCd : null;
                  object.AstKind3 = kind3.addition ? kind3.addition.KindCd : null;
              }


              object.isReverse = isReverse;

              return object;
          }
      }
      ]);



    }


    window.PTC.Select2.SingleAssets = function(elem , url , func){

        var option = {};
        option.name = elem;
        option.url = url;
        option.func = func;
        option.placeholder = "請輸入設備";
        

        window.PTC.SingleSelect2(option);

    }
    
    window.PTC.Select2.Material = function(elem , url , func){

        var option = {};
        option.name = elem;
        option.url = url;
        option.func = func;
        option.placeholder = "請輸入零件";
        

        window.PTC.SingleSelect2(option);

    }
  
})(jQuery);