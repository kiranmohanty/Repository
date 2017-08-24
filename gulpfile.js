/// <binding BeforeBuild='transpile' AfterBuild='minify' Clean='clean' ProjectOpened='watch' />
var gulp = require('gulp');
var concat = require('gulp-concat');
var uglify = require('gulp-uglify');
var del = require('del');
var Server = require('karma').Server;
var ts = require('gulp-typescript');
var gutil = require('gulp-util');

var config = {
    src: ['./src/**/*.ts', '!./src/**/*.spec.ts'],
    dest: './src/transpiled/'
};

//transpiling ts to js
gulp.task('transpile',['clean'], function () {
    gulp.src(config.src)
        .pipe(ts({
            noImplicitAny: false,
            experimentalDecorators: true,
            lib: ["es6", "dom"],
            typeRoots: [
                "../node_modules/@types/"
            ]
        }))
        .pipe(gulp.dest(config.dest));
});


//delete the output file(s)
gulp.task('clean', function () {
    return del(config.dest);
});

// Combine and minify all files from the spa folder
gulp.task('minify', function () {
    return gulp.src([config.dest+ '**/*.js'])
      .pipe(concat('spa.min.js'))
      .pipe(uglify())
      .pipe(gulp.dest(config.dest));
});

//watch for changes
gulp.task('watch', function(){
    return gulp.watch(config.src, ['transpile']);
});

//karma test runner configurations
gulp.task('UnitTest', function (done) {
    new Server({
        configFile: __dirname + '/karma.conf.js',
        singleRun: true
    }).on('browser_error', function (browser, err){
        gutil.log('Karma Run Failed: ' + err.message);
        throw err;
    }).on('run_complete', function (browsers, results){
        if (results.failed) {
            throw new Error('Karma: Tests Failed');
        }
        gutil.log('Karma Run Complete: No Failures');
        done();
    }).start();
});
