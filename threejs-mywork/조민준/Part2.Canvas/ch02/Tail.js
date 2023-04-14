import CanvasOption from "./CanvasOption.js";

export default class Tail extends CanvasOption {
  constructor(x, vy, color, radius) {
    super();
    this.x = x;
    this.y = this.canvasHeight;
    this.vy = vy;
    this.color = color;
    this.opacity = 100;
    this.radius = radius;
    this.vibrate = 90;
  }

  draw() {
    this.ctx.beginPath();
    this.ctx.arc(
      this.x + 20 * Math.cos(this.vibrate) * Math.pow(this.opacity / 100, 4),
      this.y,
      1,
      0,
      Math.PI * 2
    );
    this.ctx.fillStyle = `hsl(${this.color}, 50%, ${this.opacity}%)`;
    this.ctx.fill();
    this.ctx.closePath();
  }

  update() {
    this.draw();
    this.y += this.vy;
    this.opacity -= 1;
    this.vibrate += 0.5;
    this.vibrate %= 360;
  }
}
