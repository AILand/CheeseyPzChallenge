import React, { Fragment } from 'react';
import { Link } from 'react-router-dom';

// import { Link } from "react-router-dom";
import paths from '../routing/paths';

const NotFound: React.FC = () => {
  return (
    <Fragment>
      <h2>Oops - we've looked everywhere but couldn't find this.</h2>
      <Link to={paths.HOME}>
        <button>Return to Home page</button>
      </Link>
    </Fragment>
  );
};

export default NotFound;
