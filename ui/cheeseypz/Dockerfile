
FROM node:14-alpine AS development
ENV NODE_ENV development
WORKDIR /app

# Cache and Install dependencies
COPY package.json .
COPY yarn.lock .
RUN yarn install

# Copy app files
COPY . .

# Expose port
EXPOSE 3000

# Start the app
CMD [ "yarn", "start" ]

#docker build -f Dockerfile -t cheeseui .
#docker run -dp 3000:3000 cheeseui