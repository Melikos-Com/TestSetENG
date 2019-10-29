(function ($) {
    'use strict';

    if ($ === null || typeof $ === 'undefined') {
        console.error('jQuery required not found.');
        return;
    }


    window.PTC.Select2 = window.PTC.Select2 || {};

    window.PTC.Select2.AllArea = function (company,
                                           zo,
                                           store) {


        window.PTC.HierarchyAjaxSelect2([
          /*公司*/
          {
              elementName: company.name,
              url: company.url,
              placeholder: "請輸入公司",
              getTransData: function (isReverse) {

                  var object = {};
                  var subData = $(zo.name).select2('data')[0];
                  if (isReverse) {

                      object.CompCd = (subData) ? subData.addition.CompCd : null;

                  } else {

                  }


                  object.isReverse = isReverse;

                  return object;

              }
          },
          /*區域*/
          {
              elementName: zo.name,
              url: zo.url,
              placeholder: "請輸入區域",
              getTransData: function (isReverse) {

                  var object = {};
                  var subData = $(store.name).select2('data')[0];
                  var prevData = $(company.name).select2('data')[0];

                  if (isReverse) {

                      object.CompCd = (subData) ? subData.addition.CompCd : null;
                      object.ZoCd = (subData) ? subData.addition.ZoCd : null;

                  } else {

                      object.CompCd = prevData.addition.CompCd;

                  }
                  object.isReverse = isReverse;

                  return object;
              }

          },
          /*門市*/
          {
              elementName: store.name,
              url: store.url,
              placeholder: "請輸入門市",
              getTransData: function (isReverse) {

                  var object = {};
                  var prevData = $(zo.name).select2('data')[0];

                  if (isReverse) {


                  } else {

                      object.CompCd = prevData.addition.CompCd;
                      object.ZoCd = prevData.addition.ZoCd;

                  }

                  object.isReverse = isReverse;

                  return object;
              }
          },
        ]);

    }

    window.PTC.Select2.Area = function (company,
                                        zo) {


        window.PTC.HierarchyAjaxSelect2([
          /*公司*/
          {
              elementName: company.name,
              url: company.url,
              placeholder: "請輸入公司",
              getTransData: function (isReverse) {

                  var object = {};
                  var subData = $(zo.name).select2('data')[0];
                  if (isReverse) {

                      object.CompCd = (subData) ? subData.addition.CompCd : null;

                  } else {

                  }


                  object.isReverse = isReverse;

                  return object;

              }
          },
          /*區域*/
          {
              elementName: zo.name,
              url: zo.url,
              placeholder: "請輸入區域",
              getTransData: function (isReverse) {

                  var object = {};
                  var prevData = $(company.name).select2('data')[0];

                  if (isReverse) {
                      object.CompCd = (subData) ? subData.addition.CompCd : null;
                      object.ZoCd = (subData) ? subData.addition.ZoCd : null;

                  } else {
                      object.CompCd = prevData.addition.CompCd;
                  }
                  object.isReverse = isReverse;

                  return object;
              }

          },
        ]);

    }

    window.PTC.Select2.CV = function (company,
                                      vender) {

        window.PTC.HierarchyAjaxSelect2([
        /*公司*/
        {
            elementName: company.name,
            url: company.url,
            placeholder: "請輸入公司",
            getTransData: function (isReverse) {

                var object = {};
                var subData = $(vender.name).select2('data')[0];
                if (isReverse) {

                    object.CompCd = (subData) ? subData.addition.CompCd : null;

                } else {

                }


                object.isReverse = isReverse;

                return object;

            }
        },
        /*廠商*/
        {
            elementName: vender.name,
            url: vender.url,
            placeholder: "請輸入廠商",
            getTransData: function (isReverse) {

                var object = {};
                var prevData = $(company.name).select2('data')[0];

                if (isReverse) {


                } else {
                    object.CompCd = prevData.addition.CompCd;
                }
                object.isReverse = isReverse;

                return object;
            }

        },
        ]);


    }


    window.PTC.Select2.Vender = function (elem, url, func) {

        var option = {};
        option.name = elem;
        option.url = url;
        option.func = func;
        option.placeholder = "請輸入廠商";


        window.PTC.SingleSelect2(option);


    }

    window.PTC.Select2.Store = function (elem, url, func) {

        var option = {};
        option.name = elem;
        option.url = url;
        option.func = func;
        option.placeholder = "請輸入門市";


        window.PTC.SingleSelect2(option);


    }

    window.PTC.Select2.company = function (elem, url, func) {

        var option = {};
        option.name = elem;
        option.url = url;
        option.func = func;
        option.placeholder = "請輸入公司";


        window.PTC.SingleSelect2(option);


    }



})(jQuery);