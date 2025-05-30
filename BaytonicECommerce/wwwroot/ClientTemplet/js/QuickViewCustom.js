var main = new Splide('#main-slider', {
  type: 'fade',
  heightRatio: 0.5,
  pagination: false,
  arrows: false,
  cover: true,
});

var thumbnails = new Splide('#thumbnail-slider', {
  rewind: true,
  fixedWidth: 104,
  fixedHeight: 58,
  isNavigation: true,
  gap: 10,
  focus: 'center',
  pagination: false,
  cover: true,
  dragMinThreshold: {
    mouse: 4,
    touch: 10,
  },
  breakpoints: {
    640: {
      fixedWidth: 66,
      fixedHeight: 38,
    },
  },
});

main.sync(secondarySlider);
main.mount();
thumbnails.mount();
