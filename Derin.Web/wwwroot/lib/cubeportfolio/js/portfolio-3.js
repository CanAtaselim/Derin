(function ($, window, document, undefined)
{
    'use strict';

    // init cubeportfolio
    $('#js-grid-mosaic').cubeportfolio({
        filters: '#js-filters-mosaic',
        loadMore: '#js-loadMore-mosaic',
        loadMoreAction: 'click',
        layoutMode: 'mosaic',
        mediaQueries: [{
            width: 1000,
            cols: 7
        }, {
            width: 900,
            cols: 6
        }, {
            width: 800,
            cols: 5
        }, {
            width: 480,
            cols: 4
        }, {
            width: 320,
            cols: 2
        }],
        defaultFilter: '.cayyolu',
        animationType: 'rotateSides',
        gapHorizontal: 10,
        gapVertical: 10,
        gridAdjustment: 'responsive',
        caption: 'zoom',
        displayType: 'sequentially',
        displayTypeSpeed: 100,

        // lightbox
        lightboxDelegate: '.cbp-lightbox',
        lightboxGallery: true,
        lightboxTitleSrc: 'data-title',
        lightboxCounter: '<div class="cbp-popup-lightbox-counter">{{current}} of {{total}}</div>',

        // singlePageInline
        singlePageInlineDelegate: '.cbp-singlePageInline',
        singlePageInlinePosition: 'below'
    });
})(jQuery, window, document);
