import React from "react";
import Button from "@material-ui/core/Button";
import Box from "@material-ui/core/Box";
import Card from "@material-ui/core/Card";
import CardHeader from "@material-ui/core/CardHeader";
import CardActions from "@material-ui/core/CardActions";
import CardContent from "@material-ui/core/CardContent";
import Typography from "@material-ui/core/Typography";
import PropTypes from "prop-types";
import "./styles.css";

const MetricTile = ({ name, current, limit, goal, wish }) => (
  <Card className="card">
    <CardHeader title={name} />
    <CardContent>
      <Typography p="1px" variant color="textSecondary">
        <Box>
          <div>Current: {current}</div>
          <div>Limit: {limit}</div>
          <div>Goal: {goal}</div>
          <div>Wish: {wish}</div>
          <Box mt={1} color="text.hint" fontSize={12} textAlign="left">
            Last updated:
          </Box>
        </Box>
      </Typography>
    </CardContent>
    <CardActions>
      <Button size="small" color="primary">
        Show Details
      </Button>
    </CardActions>
  </Card>
);

MetricTile.propTypes = {
  name: PropTypes.string.isRequired,
  current: PropTypes.number.isRequired,
  limit: PropTypes.number,
  goal: PropTypes.number,
  wish: PropTypes.number,
  date: PropTypes.string
};

export default MetricTile;
