'use strict';

var gulp = require('gulp');
var sass = require('gulp-sass');
var concat = require('gulp-concat');
var cleanCSS = require('gulp-clean-css');
var flatten = require('gulp-flatten');

var libsJs = [
    'Content/www/libs/jquery/dist/jquery.js',
    'Content/www/libs/angular/angular.js',
    'Content/www/libs/angular-route/angular-route.js'
];

var styles = [
    'Content/www/libs/bootstrap/dist/css/bootstrap.css',
    'Content/www/libs/font-awesome/css/font-awesome.css'
]

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
        gulp.watch("./Content/www/Assets/**/*.scss", ['sass', 'deploy-file']);
        gulp.watch("./Content/www/App/**/*.js", ['build-js', 'deploy-file']);
    });

gulp.task('deploy-file', function () {
    gulp.src('./Content/www/**')
    .pipe(gulp.dest('../Haris.HostApp/bin/Debug/Content/www'));
});

gulp.task('deploy-file-release', function () {
    gulp.src('./Content/www/**')
    .pipe(gulp.dest('../Haris.HostApp/bin/Release/Content/www'));
});

gulp.task('default', ['deploy-file']);