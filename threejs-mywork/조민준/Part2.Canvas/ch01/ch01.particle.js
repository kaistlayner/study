const canvas = document.querySelector("canvas");

const ctx = canvas.getContext("2d");
const dpr = window.devicePixelRatio;
const canvasWidth = innerWidth;
const canvasHeight = innerHeight;

canvas.style.width = canvasWidth;
canvas.style.height = canvasHeight;
canvas.width = canvasWidth * dpr;
canvas.height = canvasHeight * dpr;
ctx.scale(dpr, dpr);

const controls = new (function () {
  this.both = 0;
  this.blurValue = 0;
  this.contrastValue = 0;
})();
let blurVal;
let contrastVal;

let gui = new dat.GUI();
gui.add(controls, "both", 0, 10).onChange((value) => {
  blurVal = value;
  contrastVal = value;
  canvas.style.filter = `blur(${blurVal}px) contrast(calc(1 + ${contrastVal}))`;
});
gui.add(controls, "blurValue", 0, 10).onChange((value) => {
  blurVal = value;
  canvas.style.filter = `blur(${blurVal}px) contrast(calc(1 + ${contrastVal}))`;
});
gui.add(controls, "contrastValue", 0, 10).onChange((value) => {
  contrastVal = value;
  canvas.style.filter = `blur(${blurVal}px) contrast(calc(1 + ${contrastVal}))`;
});

// ctx.fillRect(10, 10, 100, 100);

// ctx.beginPath();
// ctx.arc(100, 100, 50, 0, Math.PI * 2);
// ctx.fillStyle = "red";
// ctx.fill();
// // ctx.stroke();
// ctx.closePath();

class Particle {
  constructor(x, y, radius, color, velocity, acceleration, friction) {
    this.x = x;
    this.y = y;
    this.radius = radius;
    this.color = color;
    this.velocity = velocity;
    this.acceleration = acceleration;
    this.friction = friction;
  }
  draw() {
    ctx.beginPath();
    ctx.arc(this.x, this.y, this.radius, 0, Math.PI * 2);
    ctx.fillStyle = this.color;
    ctx.fill();
    ctx.closePath();
  }
  update() {
    // update y
    if (this.velocity.x * (this.velocity.x + this.acceleration.x) >= 0) {
      this.velocity.x = this.velocity.x + this.acceleration.x;
      this.x = this.x + this.velocity.x;
    } else {
      this.velocity.x = -this.velocity.x;
    }

    // collision x
    if (this.x + this.radius >= canvasWidth || this.x - this.radius <= 0) {
      this.velocity.x = -this.velocity.x * (1 - this.friction.x);
    }

    // calibrate x
    if (this.x + this.radius >= canvasWidth) {
      this.x = canvasWidth - this.radius;
    }
    if (this.x - this.radius <= 0) {
      this.x = this.radius;
    }

    // update y
    if (this.velocity.y * (this.velocity.y + this.acceleration.y) >= 0) {
      this.velocity.y = this.velocity.y + this.acceleration.y;
      this.y = this.y + this.velocity.y;
    } else {
      this.velocity.y = -this.velocity.y;
    }

    // collision y
    if (this.y + this.radius >= canvasHeight || this.y - this.radius <= 0) {
      this.velocity.y = -this.velocity.y * (1 - this.friction.y);
    }

    // calibrate y
    if (this.y + this.radius >= canvasHeight) {
      this.y = canvasHeight - this.radius;
    }
    if (this.y - this.radius <= 0) {
      this.y = this.radius;
    }

    this.draw();
  }
}

const TOTAL = 200;
const randomNumBetween = (min, max) => {
  return Math.floor(Math.random() * (max - min + 1) + min);
};

const particles = [];
for (let i = 0; i < TOTAL; i++) {
  const radius = randomNumBetween(10, 50);
  const x = randomNumBetween(radius, canvasWidth - radius);
  const y = randomNumBetween(radius, canvasHeight - radius);
  const color = `hsl(${randomNumBetween(0, 360)}, 50%, 50%)`;
  const velocity = {
    x: randomNumBetween(-2, 2),
    y: randomNumBetween(-2, 2),
  };
  const acceleration = {
    x: randomNumBetween(-2, 2),
    y: randomNumBetween(-2, 2),
  };
  const friction = {
    x: randomNumBetween(-0.2, 0.2),
    y: randomNumBetween(-0.2, 0.2),
  };
  particles.push(
    new Particle(x, y, radius, color, velocity, acceleration, friction)
  );
}

let interval = 1000 / 60;
let now, delta;
let then = Date.now();

function animate() {
  requestAnimationFrame(animate);
  now = Date.now();
  delta = now - then;
  if (delta < interval) return;

  ctx.clearRect(0, 0, canvasWidth, canvasHeight);
  particles.forEach((particle) => {
    particle.update();
  });

  then = now - (delta % interval);
}

animate();
