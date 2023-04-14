import CanvasOption from "./CanvasOption.js";

const SPEED = 1.5;

export default class Particle extends CanvasOption {
  constructor(from, to, color, velocity) {
    super();
    this.from = from;
    this.x = from.x;
    this.xInterval = (to.x - from.x) / (100 / SPEED);
    this.y = from.y;
    this.yInterval = (to.y - from.y) / (100 / SPEED);
    this.to = to;
    this.radius = 0.5;
    this.color = color;
    this.velocity = velocity;
    this.opacity = 100;
    this.gravity = 0.04;
    this.friction = 0.93;
  }

  draw() {
    this.ctx.beginPath();
    this.ctx.arc(this.x, this.y, this.radius, 0, Math.PI * 2, false);
    this.ctx.fillStyle = `hsl(${this.color}, 50%, ${this.opacity}%)`;
    this.ctx.fill();
    this.ctx.closePath();
  }

  update() {
    this.draw();
    this.x += this.velocity.x + this.xInterval;
    this.y += this.velocity.y + this.yInterval;
    this.opacity = this.opacity - SPEED;
    this.velocity.x *= this.friction;
    this.velocity.y += this.gravity;
  }
}
