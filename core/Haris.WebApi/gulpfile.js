'use strict';

var gulp = require('gulp');
var sass = require('gulp-sass');
var concat = require('gulp-concat');
var cleanCSS = require('gulp-clean-css');
var flatten = require('gulp-flatten');
var runSequence = require('run-sequence');

var libsJs = [
    'bower_components/jquery/dist/jquery.js',
    'bower_components/bootstrap/dist/js/bootstrap.js',
    'bower_components/angular/angular.js',
    'bower_components/angular-route/angular-route.js'
];

var styles = [
    'bower_components/bootstrap/dist/css/bootstrap.css',
    'bower_components/font-awesome/css/font-awesome.css'
];

gulp.task('font', function () {
    return gulp.src("bower_components/**/fonts/*")
        .pipe(flatten())
        .pipe(gulp.dest('./Content/www/fonts'));
});

gulp.task('css', function () {
    return gulp.src(styles)
        .pipe(concat('bundle.css'))
        .pipe(gulp.dest('./Content/www/dist/'));
});

gulp.task('build-js', function () {
    gulp.src(libsJs.concat(['./Content/www/App/**/*.js']))
    .pipe(concat('bundle.js'))
    .pipe(gulp.dest('./Content/www/dist/'));
});

gulp.task('watch',
    function() {
        gulp.watch("./Content/www/Assets/**/*.scss", ['deploy']);
        gulp.watch("./Content/www/App/**/*.js", ['deploy']);
    });

gulp.task('copy-file', function () {
    gulp.src('./Content/www/**')
    .pipe(gulp.dest('../Haris.HostApp/bin/Debug/Content/www'));
});

gulp.task('deploy', function () {
    runSequence("font", "build-js", "css", "copy-file", "copy-file-release");
});

gulp.task('copy-file-release', function () {
    gulp.src('./Content/www/**')
    .pipe(gulp.dest('../Haris.HostApp/bin/Release/Content/www'));
});

gulp.task('default', ['copy-file']);