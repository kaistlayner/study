import Canvas from "./Canvas.js";

const canvas = new Canvas();

addEventListener("load", () => {
  canvas.init();
  canvas.render();
});
addEventListener("resize", () => {
  canvas.init();
});
