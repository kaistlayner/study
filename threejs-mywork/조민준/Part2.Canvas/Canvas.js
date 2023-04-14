import CanvasOption from "./CanvasOption.js";
import Particle from "./Particle.js";
import Tail from "./Tail.js";

const CREATE_INTERVAL = 30;
const MAX_MULTIPLE_GENERATION = 3;

export default class Canvas extends CanvasOption {
  constructor() {
    super();
    this.particles = [];
  }

  init() {
    this.canvasWidth = innerWidth;
    this.canvasHeight = innerHeight;
    this.canvas.width = this.canvasWidth * this.dpr;
    this.canvas.height = this.canvasHeight * this.dpr;
    this.ctx.scale(this.dpr, this.dpr);

    this.canvas.style.width = this.canvasWidth + "px";
    this.canvas.style.height = this.canvasHeight + "px";

    this.tails = [];
    this.particles = [];
  }

  createTail(count) {
    const radius = Math.random() * 130 + 70;
    const x = Math.random() * (this.canvasWidth - radius * 2) + radius;
    const MIN_HEIGHT = 0.4;
    const delay = (CREATE_INTERVAL / count) * 100;

    for (let i = 0; i < count; i++) {
      setTimeout(() => {
        this.tails.push(
          new Tail(
            x,
            (-this.canvasHeight *
              (Math.random() * (1 - MIN_HEIGHT) + MIN_HEIGHT)) /
              100,
            Math.random() * 361,
            radius
          )
        );
      }, delay * i);
    }
  }

  createParticles(x, y, radius, color) {
    const newFirework = [];

    for (let i = 0; i < radius * 3; i++) {
      const degree = Math.random() * 361;
      const distance = Math.random() * (radius + 1);

      newFirework.push(
        new Particle(
          { x, y },
          {
            x: x + distance * Math.cos(degree),
            y: y + distance * Math.sin(degree),
          },
          color,
          {
            x: 0.7 * Math.cos(degree),
            y: -1.5,
          }
        )
      );
    }

    this.particles.push(newFirework);
  }

  render() {
    let now,
      delta,
      then = Date.now();
    let cnt = 0;

    const frame = () => {
      requestAnimationFrame(frame);
      now = Date.now();
      delta = now - then;
      if (delta < this.interval) return;
      this.ctx.fillStyle = "rgba(0, 0, 0, 0.05)";
      this.ctx.fillRect(0, 0, this.canvasWidth, this.canvasHeight);

      cnt++;
      if (cnt > CREATE_INTERVAL * (MAX_MULTIPLE_GENERATION / 2)) {
        this.createTail(
          Math.floor(Math.random() * MAX_MULTIPLE_GENERATION + 1)
        );
        cnt = 0;
      }

      this.tails.forEach((tail) => {
        tail.update?.();
        if (tail.opacity <= 0) {
          this.createParticles(tail.x, tail.y, tail.radius, tail.color);
          this.tails.splice(this.tails.indexOf(tail), 1);
        }
      });

      this.particles.forEach((firework) => {
        let flag = false;

        firework.forEach((particle) => {
          particle.update();
          if (particle.opacity <= 0) {
            flag = true;
          }
        });

        if (flag) {
          this.particles.splice(this.particles.indexOf(firework), 1);
          flag = false;
        }
      });

      then = now - (delta % this.interval);
    };
    requestAnimationFrame(frame);
  }
}
