(function (factory) {
    'use strict';

    if (typeof define === 'function' && define.amd) {
        define(['jquery'], factory);
    } else if (typeof exports !== 'undefined') {
        module.exports = factory(require('jquery'));
    } else {
        factory(jQuery);
    }

}
    (function ($) {
        $.fn.Toast = function (options) {
            return this.each(function () {
                var target = $(this);
                var defaults = {
                    right: parseFloat(($(window).width() - target.outerWidth()) / 2),
                    fadein: 800,
                    fadeout: 100,
                    duration: 1000
                };
                var $options = $.extend({}, defaults, options);
                target.css('left', defaults.right);
                target.fadeIn($options.fadein);
                setInterval(function () {
                    target.fadeOut($options.fadeout);
                }, parseInt($options.duration));
            });
        }
}));